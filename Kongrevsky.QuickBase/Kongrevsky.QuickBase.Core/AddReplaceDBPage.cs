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

    public class AddReplaceDBPage : IQObject
    {
        private const string QUICKBASE_ACTION = "API_AddReplaceDBPage";
        private Payload.Payload _addReplaceDBPagePayload;
        private IQUri _uri;

        public AddReplaceDBPage(string ticket, string appToken, string accountDomain, string dbid, string pageName, PageType pageType, string pageBody)
        {
            CommonConstruction(ticket, appToken, accountDomain, dbid, new AddReplaceDBPagePayload(pageName, pageType, pageBody));
        }

        public AddReplaceDBPage(string ticket, string appToken, string accountDomain, string dbid, int pageId, PageType pageType, string pageBody)
        {
            CommonConstruction(ticket, appToken, accountDomain, dbid, new AddReplaceDBPagePayload(pageId, pageType, pageBody));
        }

        private void CommonConstruction(string ticket, string appToken, string accountDomain, string dbid, Payload.Payload payload)
        {
            this._addReplaceDBPagePayload = new ApplicationTicket(payload, ticket);
            this._addReplaceDBPagePayload = new ApplicationToken(this._addReplaceDBPagePayload, appToken);
            this._addReplaceDBPagePayload = new WrapPayload(this._addReplaceDBPagePayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._addReplaceDBPagePayload.GetXmlPayload();
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
