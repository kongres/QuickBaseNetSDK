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

    public class GetDBvar : IQObject
    {
        private const string QUICKBASE_ACTION = "API_GetDBVar";
        private readonly Payload.Payload _getDBvarPayload;
        private readonly IQUri _uri;

        public GetDBvar(string ticket, string appToken, string accountDomain, string dbid, string varName)
        {
            this._getDBvarPayload = new GetDBvarPayload(varName);
            this._getDBvarPayload = new ApplicationTicket(this._getDBvarPayload, ticket);
            this._getDBvarPayload = new ApplicationToken(this._getDBvarPayload, appToken);
            this._getDBvarPayload = new WrapPayload(this._getDBvarPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._getDBvarPayload.GetXmlPayload();
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
