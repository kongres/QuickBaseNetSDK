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

    public class SendInvitation : IQObject
    {
        private const string QUICKBASE_ACTION = "API_SendInvitation";
        private Payload.Payload _sendInvitationPayload;
        private IQUri _uri;

        public SendInvitation(string ticket, string appToken, string accountDomain, string dbid, string userId, string userText)
        {
            CommonConstruction(ticket, appToken, accountDomain, dbid, new SendInvitationPayload(userId, userText));
        }

        public SendInvitation(string ticket, string appToken, string accountDomain, string dbid, string userId)
        {
            CommonConstruction(ticket, appToken, accountDomain, dbid, new SendInvitationPayload(userId));
        }

        private void CommonConstruction(string ticket, string appToken, string accountDomain, string dbid, Payload.Payload payload)
        {
            this._sendInvitationPayload = new ApplicationTicket(payload, ticket);
            this._sendInvitationPayload = new ApplicationToken(this._sendInvitationPayload, appToken);
            this._sendInvitationPayload = new WrapPayload(this._sendInvitationPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._sendInvitationPayload.GetXmlPayload();
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
