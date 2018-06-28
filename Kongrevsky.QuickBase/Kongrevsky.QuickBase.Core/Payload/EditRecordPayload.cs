﻿/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core.Payload
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml.Linq;

    internal class EditRecordPayload : Payload
    {
        private readonly int _rid;
        private readonly string _updateId;
        private readonly List<IField> _fields;
        private readonly bool _disprec;
        private readonly bool _timeInUtc;
        private readonly bool _fform;

        internal class Builder
        {
            internal Builder(int rid, List<IField> fields)
            {
                Rid = rid;
                Fields = fields;
            }

            internal int Rid { get; set; }
            internal List<IField> Fields { get; set; }

            internal string UpdateId { get; private set; }
            internal Builder SetUpdateId(string val)
            {
                UpdateId = val;
                return this;
            }

            internal bool Disprec { get; private set; }
            internal Builder SetDisprec(bool val)
            {
                Disprec = val;
                return this;
            }

            internal bool TimeInUtc { get; private set; }
            internal Builder SetTimeInUtc(bool val)
            {
                TimeInUtc = val;
                return this;
            }

            internal bool Fform { get; private set; }
            internal Builder SetFform(bool val)
            {
                Fform = val;
                return this;
            }

            internal EditRecordPayload Build()
            {
                return new EditRecordPayload(this);
            }
        }

        private EditRecordPayload(Builder builder)
        {
            this._rid = builder.Rid;
            this._updateId = builder.UpdateId;
            this._fields = builder.Fields;
            this._disprec = builder.Disprec;
            this._timeInUtc = builder.TimeInUtc;
            this._fform = builder.Fform;
        }

        internal override string GetXmlPayload()
        {
            var sb = new StringBuilder();
            sb.Append(new XElement("rid", this._rid));
            if (!string.IsNullOrEmpty(this._updateId)) sb.Append(new XElement("update_id", this._updateId));
            foreach (var field in this._fields)
            {
                if (field.Type == FieldType.file)
                {
                    XElement xelm = new XElement("field", EncodeFile(field.File));
                    xelm.SetAttributeValue("fid", field.Fid);
                    xelm.SetAttributeValue("filename", field.Value);
                    sb.Append(xelm);
                }
                else
                {
                    XElement xelm = new XElement("field", field.Value);
                    xelm.SetAttributeValue("fid", field.Fid);
                    sb.Append(xelm);
                }
            }
            if (this._disprec) sb.Append(new XElement("disprec"));
            if (this._timeInUtc) sb.Append(new XElement("msInUtc", 1));
            if (this._fform) sb.Append(new XElement("fform"));
            return sb.ToString();
        }

        private static string EncodeFile(string file)
        {
            FileStream fs = null;

            try
            {
                fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                var filebytes = new byte[fs.Length];
                fs.Read(filebytes, 0, Convert.ToInt32(fs.Length));
                return Convert.ToBase64String(filebytes, Base64FormattingOptions.None);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
    }
}