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

    public class ChangeRecordOwner : IQObject
    {
        private const string QUICKBASE_ACTION = "API_ChangeRecordOwner";
        private readonly Payload.Payload _changeRecordOwnerPayload;
        private readonly IQUri _uri;

        public ChangeRecordOwner(string ticket, string appToken, string accountDomain, string dbid, int rid, string newOwner)
        {
            this._changeRecordOwnerPayload = new ChangeRecordOwnerPayload(rid, newOwner);
            this._changeRecordOwnerPayload = new ApplicationTicket(this._changeRecordOwnerPayload, ticket);
            this._changeRecordOwnerPayload = new ApplicationToken(this._changeRecordOwnerPayload, appToken);
            this._changeRecordOwnerPayload = new WrapPayload(this._changeRecordOwnerPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._changeRecordOwnerPayload.GetXmlPayload();
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
