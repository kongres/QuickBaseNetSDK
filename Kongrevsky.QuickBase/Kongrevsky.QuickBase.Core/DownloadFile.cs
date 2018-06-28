﻿/*
 * Copyright © 2013 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core
{
    using System;

    public class DownloadFile
    {
        private string _ticket;
        private string _accountDomain;
        private string _path;
        private string _file;
        private string _tableId;
        private int _recordId;
        private int _fieldId;
        private int _versionId;

        public DownloadFile(string ticket, string accountDomain, string path, string file, string tableId, int recordId, int fieldId)
            : this(ticket, accountDomain, path, file, tableId, recordId, fieldId, 0)
        {
        }

        public DownloadFile(string ticket, string accountDomain, string path, string file, string tableId, int recordId, int fieldId, int versionId)
        {
            Ticket = ticket;
            AccountDomain = accountDomain;
            Path = path;
            File = file;
            TableId = tableId;
            RecordId = recordId;
            FieldId = fieldId;
            VersionId = versionId;
        }

        public string Ticket
        {
            get { return this._ticket; }
            set
            {
                if (value == null) throw new ArgumentNullException("ticket");
                if (value.Trim() == String.Empty) throw new ArgumentException("ticket");
                this._ticket = value;
            }
        }

        public string AccountDomain
        {
            get { return this._accountDomain; }
            set
            {
                if (value == null) throw new ArgumentNullException("accountDomain");
                if (value.Trim() == String.Empty) throw new ArgumentException("accountDomain");
                this._accountDomain = value;
            }
        }

        public string Path
        {
            get { return this._path; }
            set
            {
                if (value == null) throw new ArgumentNullException("path");
                if (value.Trim() == String.Empty) throw new ArgumentException("path");
                this._path = value;
            }
        }

        public string File
        {
            get { return this._file; }
            set
            {
                if (value == null) throw new ArgumentNullException("file");
                if (value.Trim() == String.Empty) throw new ArgumentException("file");
                this._file = value;
            }
        }

        private string TableId
        {
            get { return this._tableId; }
            set
            {
                if (value == null) throw new ArgumentNullException("tableId");
                if (value.Trim() == String.Empty) throw new ArgumentException("tableId");
                this._tableId = value;
            }
        }

        private int RecordId
        {
            get { return this._recordId; }
            set
            {
                if (value < 1) throw new ArgumentException("recordId");
                this._recordId = value;
            }
        }

        private int FieldId
        {
            get { return this._fieldId; }
            set
            {
                if (value < 1) throw new ArgumentException("fieldId");
                this._fieldId = value;
            }
        }

        private int VersionId
        {
            get { return this._versionId; }
            set
            {
                if (value < 0) throw new ArgumentException("versionId");
                this._versionId = value;
            }
        }

        public string Uri
        {
            get
            {
                return String.Format("https://{0}/up/{1}/a/r{2}/e{3}/v{4}",
                    AccountDomain, TableId, RecordId, FieldId, VersionId);
            }
        }

        public void Get()
        {
            new Http().GetFile(this);
        }
    }
}