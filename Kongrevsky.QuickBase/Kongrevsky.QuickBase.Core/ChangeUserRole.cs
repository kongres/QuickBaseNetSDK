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

    public class ChangeUserRole : IQObject
    {
        private const string QUICKBASE_ACTION = "API_ChangeUserRole";
        private Payload.Payload _changeUserRolePayload;
        private IQUri _uri;

        public ChangeUserRole(string ticket, string appToken, string accountDomain, string dbid, string userId, int currentRoleId, int newRoldId)
        {
            CommonConstruction(ticket, appToken, accountDomain, dbid, new ChangeUserRolePayload(userId, currentRoleId, newRoldId));

        }

        public ChangeUserRole(string ticket, string appToken, string accountDomain, string dbid, string userId, int currentRoleId)
        {
            CommonConstruction(ticket, appToken, accountDomain, dbid, new ChangeUserRolePayload(userId, currentRoleId));
        }

        private void CommonConstruction(string ticket, string appToken, string accountDomain, string dbid, Payload.Payload payload)
        {
            this._changeUserRolePayload = new ApplicationTicket(payload, ticket);
            this._changeUserRolePayload = new ApplicationToken(this._changeUserRolePayload, appToken);
            this._changeUserRolePayload = new WrapPayload(this._changeUserRolePayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._changeUserRolePayload.GetXmlPayload();
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
