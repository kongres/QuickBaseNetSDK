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

    public class RemoveUserFromRole : IQObject
    {
        private const string QUICKBASE_ACTION = "API_RemoveUserFromRole";
        private readonly Payload.Payload _removeUserFromRolePayload;
        private readonly IQUri _uri;

        public RemoveUserFromRole(string ticket, string appToken, string accountDomain, string dbid, string userId, int roleId)
        {
            this._removeUserFromRolePayload = new RemoveUserFromRolePayload(userId, roleId);
            this._removeUserFromRolePayload = new ApplicationTicket(this._removeUserFromRolePayload, ticket);
            this._removeUserFromRolePayload = new ApplicationToken(this._removeUserFromRolePayload, appToken);
            this._removeUserFromRolePayload = new WrapPayload(this._removeUserFromRolePayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._removeUserFromRolePayload.GetXmlPayload();
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
