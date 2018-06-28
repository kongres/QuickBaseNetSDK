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
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Kongrevsky.QuickBase.Core.Payload;
    using Kongrevsky.QuickBase.Core.Uri;

    /// <summary>
    /// You invoke this call on a table-level dbid to add a record to that table by specifying 
    /// fields and their values. You don’t have to set all field values but you must set 
    /// all required fields or the call returns an error. Unsupported tags: &lt;field=""&gt;&lt;/field&gt;, 
    /// &lt;disprec&gt;&lt;/disprec&gt;, &lt;fform&gt;&lt;/fform&gt;, &lt;ignoreError&gt;&lt;/ignoreError&gt; and
    /// &lt;udata&gt;&lt;/udata&gt;
    /// </summary>
    public class AddRecord : IQObject
    {
        private const string QUICKBASE_ACTION = "API_AddRecord";
        private readonly Payload.Payload _addRecordPayload;
        private readonly IQUri _uri;

        public class Builder
        {
            private List<IField> _fields;

            internal string Ticket { get; set; }
            internal string AppToken { get; set; }
            internal string AccountDomain { get; set; }
            internal string Dbid { get; set; }
            internal List<IField> Fields
            {
                get
                {
                    return this._fields;
                }
                set
                {
                    if (value == null) throw new ArgumentNullException("fields");
                    this._fields = value;
                }
            }

            public Builder(string ticket, string appToken, string accountDomain, string dbid, List<IField> fields)
            {
                Ticket = ticket;
                AppToken = appToken;
                AccountDomain = accountDomain;
                Dbid = dbid;
                Fields = fields;
            }

            internal bool Disprec { get; private set; }

            public Builder SetDisprec(bool val)
            {
                Disprec = val;
                return this;
            }

            internal bool TimeInUtc { get; private set; }

            public Builder SetTimeInUtc(bool val)
            {
                TimeInUtc = val;
                return this;
            }

            internal bool Fform { get; private set; }

            public Builder SetFform(bool val)
            {
                Fform = val;
                return this;
            }

            public AddRecord Build()
            {
                return new AddRecord(this);
            }
        }

        private AddRecord(Builder builder)
        {
            this._addRecordPayload = new AddRecordPayload.Builder(builder.Fields)
                .SetDisprec(builder.Disprec)
                .SetTimeInUtc(builder.TimeInUtc)
                .SetFform(builder.Fform)
                .Build();
            this._addRecordPayload = new ApplicationTicket(this._addRecordPayload, builder.Ticket);
            this._addRecordPayload = new ApplicationToken(this._addRecordPayload, builder.AppToken);
            this._addRecordPayload = new WrapPayload(this._addRecordPayload);
            this._uri = new QUriDbid(builder.AccountDomain, builder.Dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._addRecordPayload.GetXmlPayload();
            }
        }

        public System.Uri Uri
        {
            get
            {
                return this._uri.GetQUri();
            }
        }

        public string Action
        {
            get
            {
                return QUICKBASE_ACTION;
            }
        }

        public XPathDocument Post()
        {
            HttpPost httpXml = new HttpPostXml();
            httpXml.Post(this);
            return httpXml.Response;
        }
    }
}