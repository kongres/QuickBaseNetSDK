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

    public class GetUserRole : IQObject
    {
        private const string QUICKBASE_ACTION = "API_GetUserRole";
        private readonly Payload.Payload _getUserRolePayload;
        private readonly IQUri _uri;

        public GetUserRole(string ticket, string appToken, string accountDomain, string dbid, string userId)
        {
            this._getUserRolePayload = new GetUserRolePayload(userId);
            this._getUserRolePayload = new ApplicationTicket(this._getUserRolePayload, ticket);
            this._getUserRolePayload = new ApplicationToken(this._getUserRolePayload, appToken);
            this._getUserRolePayload = new WrapPayload(this._getUserRolePayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._getUserRolePayload.GetXmlPayload();
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
