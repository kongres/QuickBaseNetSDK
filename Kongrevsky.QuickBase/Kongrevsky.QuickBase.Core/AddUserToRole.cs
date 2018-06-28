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

    public class AddUserToRole : IQObject
    {
        private const string QUICKBASE_ACTION = "API_AddUserToRole";
        private readonly Payload.Payload _addUserToRolePayload;
        private readonly IQUri _uri;

        public AddUserToRole(string ticket, string appToken, string accountDomain, string dbid, string userId, int roleId)
        {
            this._addUserToRolePayload = new AddUserToRolePayload(userId, roleId);
            this._addUserToRolePayload = new ApplicationTicket(this._addUserToRolePayload, ticket);
            this._addUserToRolePayload = new ApplicationToken(this._addUserToRolePayload, appToken);
            this._addUserToRolePayload = new WrapPayload(this._addUserToRolePayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._addUserToRolePayload.GetXmlPayload();
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
