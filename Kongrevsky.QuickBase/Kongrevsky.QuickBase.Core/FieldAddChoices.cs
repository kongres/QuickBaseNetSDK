/*
 * Copyright © 2013 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core
{
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Kongrevsky.QuickBase.Core.Payload;
    using Kongrevsky.QuickBase.Core.Uri;

    public class FieldAddChoices : IQObject
    {
        private const string QUICKBASE_ACTION = "API_FieldAddChoices";
        private readonly Payload.Payload _fieldAddChoicesPayload;
        private readonly IQUri _uri;

        public FieldAddChoices(string ticket, string appToken, string accountDomain, string dbid, int fid, List<string> choices)
        {
            this._fieldAddChoicesPayload = new FieldChoicesPayload(fid, choices);
            this._fieldAddChoicesPayload = new ApplicationTicket(this._fieldAddChoicesPayload, ticket);
            this._fieldAddChoicesPayload = new ApplicationToken(this._fieldAddChoicesPayload, appToken);
            this._fieldAddChoicesPayload = new WrapPayload(this._fieldAddChoicesPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._fieldAddChoicesPayload.GetXmlPayload();
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
