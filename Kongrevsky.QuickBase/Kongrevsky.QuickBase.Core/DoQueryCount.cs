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
    using System.Xml.XPath;
    using Kongrevsky.QuickBase.Core.Payload;
    using Kongrevsky.QuickBase.Core.Uri;

    public class DoQueryCount : IQObject
    {
        private const string QUICKBASE_ACTION = "API_DoQueryCount";
        private readonly Payload.Payload _doQueryCountPayload;
        private readonly IQUri _uri;

        public class Builder
        {
            internal string Ticket { get; set; }
            internal string AppToken { get; set; }
            internal string AccountDomain { get; set; }
            internal string Dbid { get; set; }

            public Builder(string ticket, string appToken, string accountDomain, string dbid)
            {
                Ticket = ticket;
                AppToken = appToken;
                AccountDomain = accountDomain;
                Dbid = dbid;
            }

            internal string Query { get; private set; }

            public Builder SetQuery(string val)
            {
                if (val == null) throw new ArgumentNullException("query");
                if (val.Trim() == String.Empty) throw new ArgumentException("query");
                Query = val;
                return this;
            }

            internal int Qid { get; private set; }

            public Builder SetQid(int val)
            {
                if (val < 1) throw new ArgumentException("qid");
                Qid = val;
                return this;
            }

            internal string QName { get; private set; }

            public Builder SetQName(string val)
            {
                if (val == null) throw new ArgumentNullException("qName");
                if (val.Trim() == String.Empty) throw new ArgumentException("qName");
                QName = val;
                return this;
            }

            public DoQueryCount Build()
            {
                return new DoQueryCount(this);
            }
        }

        private DoQueryCount(Builder builder)
        {
            this._doQueryCountPayload = new DoQueryCountPayload.Builder()
                .SetQuery(builder.Query)
                .SetQid(builder.Qid)
                .SetQName(builder.QName)
                .Build();
            this._doQueryCountPayload = new ApplicationTicket(this._doQueryCountPayload, builder.Ticket);
            this._doQueryCountPayload = new ApplicationToken(this._doQueryCountPayload, builder.AppToken);
            this._doQueryCountPayload = new WrapPayload(this._doQueryCountPayload);
            this._uri = new QUriDbid(builder.AccountDomain, builder.Dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._doQueryCountPayload.GetXmlPayload();
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