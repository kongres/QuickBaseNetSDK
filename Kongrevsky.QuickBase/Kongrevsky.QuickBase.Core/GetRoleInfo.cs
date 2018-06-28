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

    public class GetRoleInfo : IQObject
    {
        private const string QUICKBASE_ACTION = "API_GetRoleInfo";
        private readonly Payload.Payload _getRoleInfoPayload;
        private readonly IQUri _uri;

        public GetRoleInfo(string ticket, string appToken, string accountDomain, string dbid)
        {
            this._getRoleInfoPayload = new GetRoleInfoPayload();
            this._getRoleInfoPayload = new ApplicationTicket(this._getRoleInfoPayload, ticket);
            this._getRoleInfoPayload = new ApplicationToken(this._getRoleInfoPayload, appToken);
            this._getRoleInfoPayload = new WrapPayload(this._getRoleInfoPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._getRoleInfoPayload.GetXmlPayload();
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
