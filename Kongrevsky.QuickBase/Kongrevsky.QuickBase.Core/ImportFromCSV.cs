/*
 * Copyright © 2013 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core
{
    using System;
    using System.Xml.XPath;
    using Kongrevsky.QuickBase.Core.Payload;
    using Kongrevsky.QuickBase.Core.Uri;

    public class ImportFromCSV : IQObject
    {
        private const string QUICKBASE_ACTION = "API_ImportFromCSV";
        private readonly Payload.Payload _importFromCSVPayload;
        private readonly IQUri _uri;

        public class Builder
        {
            private string _recordsCsv;

            internal string Ticket { get; set; }
            internal string AppToken { get; set; }
            internal string AccountDomain { get; set; }
            internal string Dbid { get; set; }
            internal string RecordsCsv
            {
                get
                {
                    return this._recordsCsv;
                }
                set
                {
                    if (value == null) throw new ArgumentNullException("recordsCsv");
                    if (value.Trim() == String.Empty) throw new ArgumentException("recordsCsv");
                    this._recordsCsv = value;
                }
            }

            public Builder(string ticket, string appToken, string accountDomain, string dbid, string recordsCsv)
            {
                Ticket = ticket;
                AppToken = appToken;
                AccountDomain = accountDomain;
                Dbid = dbid;
                RecordsCsv = recordsCsv;
            }

            internal string CList { get; private set; }

            public Builder SetCList(string val)
            {
                if (val == null) throw new ArgumentNullException("cList");
                if (val.Trim() == String.Empty) throw new ArgumentException("cList");
                CList = val;
                return this;
            }

            internal bool SkipFirst { get; private set; }

            public Builder SetSkipFirst(bool val)
            {
                SkipFirst = val;
                return this;
            }

            internal bool TimeInUtc { get; private set; }

            public Builder SetTimeInUtc(bool val)
            {
                TimeInUtc = val;
                return this;
            }

            public ImportFromCSV Build()
            {
                return new ImportFromCSV(this);
            }
        }

        private ImportFromCSV(Builder builder)
        {
            this._importFromCSVPayload = new ImportFromCSVPayload.Builder(builder.RecordsCsv)
                .SetCList(builder.CList)
                .SetSkipFirst(builder.SkipFirst)
                .SetTimeInUtc(builder.TimeInUtc)
                .Build();
            this._importFromCSVPayload = new ApplicationTicket(this._importFromCSVPayload, builder.Ticket);
            this._importFromCSVPayload = new ApplicationToken(this._importFromCSVPayload, builder.AppToken);
            this._importFromCSVPayload = new WrapPayload(this._importFromCSVPayload);
            this._uri = new QUriDbid(builder.AccountDomain, builder.Dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._importFromCSVPayload.GetXmlPayload();
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
