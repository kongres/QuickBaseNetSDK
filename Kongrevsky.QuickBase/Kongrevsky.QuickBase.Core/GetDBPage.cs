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

    public class GetDBPage : IQObject
    {
        private const string QUICKBASE_ACTION = "API_GetDBPage";
        private readonly Payload.Payload _getDbPagePayload;
        private readonly IQUri _uri;

        public GetDBPage(string ticket, string appToken, string accountDomain, string dbid, int pageId)
        {
            this._getDbPagePayload = new GetDBPagePayload(pageId);
            this._getDbPagePayload = new ApplicationTicket(this._getDbPagePayload, ticket);
            this._getDbPagePayload = new ApplicationToken(this._getDbPagePayload, appToken);
            this._getDbPagePayload = new WrapPayload(this._getDbPagePayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._getDbPagePayload.GetXmlPayload();
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
