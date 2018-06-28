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

    public class UserRoles : IQObject
    {
        private const string QUICKBASE_ACTION = "API_UserRoles";
        private readonly Payload.Payload _userRolesPayload;
        private readonly IQUri _uri;

        public UserRoles(string ticket, string appToken, string accountDomain, string dbid)
        {
            this._userRolesPayload = new UserRolesPayload();
            this._userRolesPayload = new ApplicationTicket(this._userRolesPayload, ticket);
            this._userRolesPayload = new ApplicationToken(this._userRolesPayload, appToken);
            this._userRolesPayload = new WrapPayload(this._userRolesPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._userRolesPayload.GetXmlPayload();
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
