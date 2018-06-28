/*
 * Copyright © 2013 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core
{
    using System.Xml.XPath;
    using Kongrevsky.QuickBase.Core.Payload;
    using Kongrevsky.QuickBase.Core.Uri;

    public class RunImport : IQObject
    {
        private const string QUICKBASE_ACTION = "API_runimport";
        private readonly Payload.Payload _runImportPayload;
        private readonly IQUri _uri;

        public RunImport(string ticket, string appToken, string accountDomain, string dbid, int id)
        {
            this._runImportPayload = new RunImportPayload(id);
            this._runImportPayload = new ApplicationTicket(this._runImportPayload, ticket);
            this._runImportPayload = new ApplicationToken(this._runImportPayload, appToken);
            this._runImportPayload = new WrapPayload(this._runImportPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._runImportPayload.GetXmlPayload();
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
