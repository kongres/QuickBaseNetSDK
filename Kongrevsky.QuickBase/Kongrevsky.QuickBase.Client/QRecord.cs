/*
 * Copyright © 2013 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Client
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.XPath;
    using Kongrevsky.QuickBase.Core;
    using Kongrevsky.QuickBase.Core.Exceptions;

    public class QRecord : IQRecord, IQRecord_int
    {
        // Instance fields
        private readonly List<QField> _fields;
        private RecordState _recordState = RecordState.New;
        private bool _unclean;

        // Constructors
        internal QRecord(IQApplication application, IQTable table, QColumnCollection columns)
        {
            Application = application;
            Table = table;
            Columns = columns;
            this._fields = new List<QField>();
        }

        internal QRecord(IQApplication application, IQTable table, QColumnCollection columns, XPathNavigator recordNode)
            : this(application, table, columns)
        {
            FillRecord(recordNode);
        }

        // Properties
        internal IQApplication Application { get; set; }
        internal IQTable Table { get; set; }
        internal QColumnCollection Columns { get; set; }

        public bool IsOnServer { get; private set; }
        public int RecordId { get; private set; }

        public bool UncleanState
        {
            get
            {
                return this._unclean;
            }
            internal set
            {
                this._unclean = value;
            }
        }

        public RecordState RecordState
        {
            get
            {
                return this._recordState;
            }
            internal set
            {
                this._recordState = value;
            }
        }

        private void FieldLoad(int index, string value)
        {
            // Get field location with column index
            var fieldIndex = this._fields.IndexOf(new QField(Columns[index].ColumnId));

            if (fieldIndex > -1)
            {
                SetExistingField(index, fieldIndex, value);
            }
            else
            {
                CreateNewField(index, value, true);
            }            
        }

        public object this[IQColumn column]
        {
            get
            {
                // Get field location with column index
                var qField = new QField(column.ColumnId);
                var fieldIndex = this._fields.IndexOf(qField);

                if (fieldIndex == -1)
                {
                    //make null field
                    CreateNewField(column.ColumnId, null, false);
                    fieldIndex = this._fields.IndexOf(qField);
                }
                // Return field with column index
                return this._fields[fieldIndex].Value;
            }

            set
            {
                // Get field location with column index
                var fieldIndex = this._fields.IndexOf(new QField(column.ColumnId));

                if(fieldIndex > -1)
                {
                    SetExistingField(column.ColumnId, fieldIndex, value);
                }
                else
                {
                    CreateNewField(column.ColumnId, value, false);
                }
            }
        }

        public object this[int columnId]
        {
            get
            {
                // Get field location with column index
                var qField = new QField(columnId);
                var fieldIndex = this._fields.IndexOf(qField);

                if (fieldIndex == -1)
                {
                    //make null field
                    CreateNewField(columnId, null, false);
                    fieldIndex = this._fields.IndexOf(qField);
                }
                // Return field with column index
                return this._fields[fieldIndex].Value;
            }

            set
            {
                // Get field location with column index
                var fieldIndex = this._fields.IndexOf(new QField(columnId));

                if(fieldIndex > -1)
                {
                    SetExistingField(columnId, fieldIndex, value);
                }
                else
                {
                    CreateNewField(columnId, value, false);
                }
            }
        }

        public object this[string columnName]
        {
            get
            {
                // Get column index
                var index = GetColumnIndex(columnName);

                // Get field location with column index
                var fieldIndex = this._fields.IndexOf(new QField(Columns[index].ColumnId));

                if (fieldIndex == -1)
                {
                    //make null field
                    CreateNewField(index, null, false);
                    fieldIndex = this._fields.IndexOf(new QField(Columns[index].ColumnId));
                }
                // Return field with column index
                return this._fields[fieldIndex].Value;
            }
            set
            {
                // Get column index
                var index = GetColumnIndex(columnName);

                // Get field location with column index
                var fieldIndex = this._fields.IndexOf(new QField(Columns[index].ColumnId));

                if (fieldIndex > -1)
                {
                    SetExistingField(index, fieldIndex, value);
                }
                else
                {
                    CreateNewField(index, value, false);
                }
            }
        }

        public void AcceptChanges()
        {
            var fieldsToPost = new List<IField>();

            if(RecordState == RecordState.Modified)
            {
                foreach (var field in this._fields)
                {
                    if (field.Column.ColumnLookup) continue; //don't try to update values that are results of lookups
                    if (field.Update)
                    {
                        IField qField = new Field(field.FieldId, field.Type, field.QBValue);
                        if(field.Type == FieldType.file)
                        {
                            qField.File = field.FullName;
                        }
                        fieldsToPost.Add(qField);
                        field.Update = false;
                    }
                }
                var editBuilder = new EditRecord.Builder(Application.Client.Ticket, Application.Token, Application.Client.AccountDomain, Table.TableId, RecordId, fieldsToPost);
                editBuilder.SetTimeInUtc(true);
                var editRecord = editBuilder.Build();
                editRecord.Post();
                RecordState = RecordState.Unchanged;
            }
            else if(RecordState == RecordState.New)
            {
                foreach (var field in this._fields)
                {
                    if (field.Column.ColumnLookup) continue; //don't try to update values that are results of lookups
                    IField qField = new Field(field.FieldId, field.Type, field.QBValue);
                    if (field.Type == FieldType.file)
                    {
                        qField.File = field.FullName;
                    }
                    fieldsToPost.Add(qField);
                }
                var addBuilder = new AddRecord.Builder(Application.Client.Ticket, Application.Token, Application.Client.AccountDomain, Table.TableId, fieldsToPost);
                addBuilder.SetTimeInUtc(true);
                var addRecord = addBuilder.Build();
                RecordState = RecordState.Unchanged;

                var xml = addRecord.Post().CreateNavigator();
                RecordId = int.Parse(xml.SelectSingleNode("/qdbapi/rid").Value);
                RecordState = RecordState.Unchanged;
                IsOnServer = true;
            }
        }

        public string GetAsCSV(string clist)
        {
            List<string> csvList = new List<string>();
            List<int> cols = clist.Split('.').Select(Int32.Parse).ToList();
            foreach (int col in cols)
            {
                QField field = this._fields.FirstOrDefault(fld => fld.FieldId == col);
                if (field == null)
                {
                    csvList.Add(String.Empty);
                }
                else
                {                    
                    if (field.Type == FieldType.file) throw new InvalidChoiceException(); //Can't upload a file via CSV upload
                    csvList.Add(CSVQuoter(field.QBValue));
                }
            }
            return String.Join(",", csvList);
        }

        private string CSVQuoter(string inStr)
        {
            //if the string contains quote character(s), newlines or commas, surround the string with quotes, and double quotes if present
            if (inStr.Contains("\"") || inStr.Contains(",") || inStr.Contains("\n") || inStr.Contains("\r"))
                return "\"" + inStr.Replace("\"","\"\"") + "\"";
            else
                return inStr;
        }

        public void ForceUpdateState(int recId)
        {
            RecordState = RecordState.Unchanged;
            IsOnServer = true;
            RecordId = recId;
        }

        public void ForceUpdateState()
        {
            RecordState = RecordState.Unchanged;
            IsOnServer = true;
        }

        private void FillRecord(XPathNavigator recordNode)
        {
            IsOnServer = true;
            var fieldNodes = recordNode.Select("f");
            var colIndex = 0;
            foreach (XPathNavigator fieldNode in fieldNodes)
            {
                if (fieldNode.HasChildren && fieldNode.MoveToChild("url", String.Empty))
                {
                    fieldNode.MoveToFirst();
                    FieldLoad(colIndex, fieldNode.TypedValue as string);
                }
                else
                {
                    FieldLoad(colIndex, fieldNode.TypedValue as string);
                }

                if (fieldNode.GetAttribute("id", String.Empty).Equals("3"))
                {
                    RecordId = fieldNode.ValueAsInt;
                }
                colIndex++;
            }
            RecordState = RecordState.Unchanged;
        }

        public void UploadFile(string columnName, string filePath)
        {
            // create new field with columnName
            var index = GetColumnIndex(columnName);
            CreateNewField(index, columnName, false);
            
            // change type to file
            Columns[index].ColumnType = FieldType.file;
            
            // Get field location with column index
            var fieldIndex = this._fields.IndexOf(new QField(Columns[index].ColumnId));
            SetExistingField(index, fieldIndex, filePath);
        }

        public void DownloadFile(string columnName, string path, int versionId)
        {
            var index = GetColumnIndex(columnName);
            var field = this._fields[this._fields.IndexOf(new QField(Columns[index].ColumnId))];
            string fileName = (string)field.Value;

            var fileToDownload = new DownloadFile(Application.Client.Ticket, Application.Client.AccountDomain, path, fileName, Table.TableId, RecordId,
                                                           field.FieldId, versionId);
            fileToDownload.Get();
        }

        public void ChangeOwnerTo(string newOwner)
        {
            var changeRecordOwner = new ChangeRecordOwner(Application.Client.Ticket, Application.Token, Application.Client.AccountDomain, Table.TableId, RecordId, newOwner);
            changeRecordOwner.Post();
        }

        public bool Equals(IQRecord record)
        {
            if (ReferenceEquals(null, record)) return false;
            if (ReferenceEquals(this, record)) return true;
            return record.RecordId == RecordId;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (QRecord)) return false;
            return Equals((IQRecord) obj);
        }

        public override int GetHashCode()
        {
            return RecordId;
        }

        public override string ToString()
        {
            return RecordId.ToString();
        }

        //value 'QBInternal' is passed into the QField, and specifies if the column exists inside QB's internal dataformat, or is .Net datatype
        private void CreateNewField(int index, object value, bool QBInternal)
        {
            if (Columns[index].ColumnType == FieldType.file && !IsOnServer)
            {
                string fileName = (string)value;
                var field = new QField(Columns[index].ColumnId, Path.GetFileName(fileName), Columns[index].ColumnType, this, Columns[index], QBInternal)
                {
                    FullName = fileName
                };
                this._fields.Add(field);
            }
            else
            {
                var field = new QField(Columns[index].ColumnId, value, Columns[index].ColumnType, this, Columns[index], QBInternal);
                this._fields.Add(field);
            }
            UncleanState = this._fields.Any(f => f.UncleanText == true);
        }

        private void SetExistingField(int index, int fieldIndex, object value)
        {
            if (Columns[index].ColumnType == FieldType.file)
            {
                string fileName = (string) value;
                this._fields[fieldIndex].Value = Path.GetFileName(fileName);
                this._fields[fieldIndex].FullName = fileName;
                if (RecordState != RecordState.New)
                {
                    RecordState = RecordState.Modified;
                }
            }
            else
            {
                if (this._fields[fieldIndex].Value == null || !this._fields[fieldIndex].Value.Equals(value))
                {
                    this._fields[fieldIndex].Value = value;
                    if (RecordState != RecordState.New)
                    {
                        RecordState = RecordState.Modified;
                    }
                }
            }
            UncleanState = this._fields.Any(f => f.UncleanText == true);
        }

        public int GetColumnIndex(string columnName)
        {
            var index = Columns.IndexOf(new QColumn
            {
                ColumnName = columnName
            });
            if (index == -1)
            {
                throw new ColumnDoesNotExistInTableExecption(string.Format("Column '{0}' not found in table.", columnName));
            }
            return index;
        }
    }
}