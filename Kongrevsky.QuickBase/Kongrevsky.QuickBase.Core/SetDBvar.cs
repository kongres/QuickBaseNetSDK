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

    public class SetDBvar : IQObject
    {
        private const string QUICKBASE_ACTION = "API_SetDBVar";
        private readonly Payload.Payload _setDbVarPayload;
        private readonly IQUri _uri;

        public SetDBvar(string ticket, string appToken, string accountDomain, string dbid, string varName, string value)
        {
            this._setDbVarPayload = new SetDBvarPayload(varName, value);
            this._setDbVarPayload = new ApplicationTicket(this._setDbVarPayload, ticket);
            this._setDbVarPayload = new ApplicationToken(this._setDbVarPayload, appToken);
            this._setDbVarPayload = new WrapPayload(this._setDbVarPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._setDbVarPayload.GetXmlPayload();
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
