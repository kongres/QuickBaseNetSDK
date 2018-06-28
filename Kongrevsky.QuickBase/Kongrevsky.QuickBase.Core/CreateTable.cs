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

    /// <summary>
    /// If you have application administration rights, you can use this call to create 
    /// a child table for that application. The dbid you supply must be an application-level 
    /// dbid, not a table-level dbid. The tname parameter is where you specify the table 
    /// name. Each table name must be unique within the application. If you supply a 
    /// duplicate name (or use the default more than once) you will get an error. The 
    /// pnoun parameter is where you specify the name for each record. Unsupported tags:  
    /// &lt;pnoun&gt;&lt;/pnoun&gt; and &lt;udata&gt;&lt;/udata&gt;
    /// </summary>
    public class CreateTable : IQObject
    {
        private const string QUICKBASE_ACTION = "API_CreateTable";
        private readonly Payload.Payload _createTablePayload;
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

            internal string TName { get; private set; }

            public Builder SetTName(string val)
            {
                if (val == null) throw new ArgumentNullException("tName");
                if (val.Trim() == String.Empty) throw new ArgumentException("tName");
                TName = val;
                return this;
            }

            internal string PNoun { get; private set; }

            public Builder SetPNoun(string val)
            {
                if (val == null) throw new ArgumentNullException("pNoun");
                if (val.Trim() == String.Empty) throw new ArgumentException("pNoun");
                PNoun = val;
                return this;
            }

            public CreateTable Build()
            {
                return new CreateTable(this);
            }
        }

        private CreateTable(Builder builder)
        {
            this._createTablePayload = new CreateTablePayload.Builder()
                .SetTName(builder.TName)
                .SetPNoun(builder.PNoun)
                .Build();
            this._createTablePayload = new ApplicationTicket(this._createTablePayload, builder.Ticket);
            this._createTablePayload = new ApplicationToken(this._createTablePayload, builder.AppToken);
            this._createTablePayload = new WrapPayload(this._createTablePayload);
            this._uri = new QUriDbid(builder.AccountDomain, builder.Dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._createTablePayload.GetXmlPayload();
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