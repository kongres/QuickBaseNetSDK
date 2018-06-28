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

    /// <summary>
    /// If you invoke this call on an application-level dbid, it returns metadata information about the application, 
    /// such as any DBVars created for it and all child table dbids as well. If you invoke this call on a table-level 
    /// dbid, the DBVars are also listed, but there will additionally be table-related information such as queries, 
    /// field IDs (fid), and the current property settings for each field. Unsupported tag: &lt;udata&gt;&lt;/udata&gt;
    /// </summary>
    public class GetSchema : IQObject
    {
        private const string QUICKBASE_ACTION = "API_GetSchema";
        private readonly Payload.Payload _getRecordInfoPayload;
        private readonly IQUri _uri;

        /// <summary>
        /// Initializes a new instance of the com.intuit.quickbase.API_GetSchema class.
        /// </summary>
        /// <param name="ticket">Supply auth ticket for application access. See com.intuit.quickbase.API_Authenticate class to obtain a ticket.</param>
        /// <param name="appToken">Supply application token that is assigned to your QuickBase Application. See QuickBase Online help to obtain an application token.</param>
        /// <param name="accountDomain"></param>
        /// <param name="dbid">Supply application-level or table-level dbid.</param>
        public GetSchema(string ticket, string appToken, string accountDomain, string dbid)
        {
            this._getRecordInfoPayload = new GetSchemaPayload();
            this._getRecordInfoPayload = new ApplicationTicket(this._getRecordInfoPayload, ticket);
            this._getRecordInfoPayload = new ApplicationToken(this._getRecordInfoPayload, appToken);
            this._getRecordInfoPayload = new WrapPayload(this._getRecordInfoPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._getRecordInfoPayload.GetXmlPayload();
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