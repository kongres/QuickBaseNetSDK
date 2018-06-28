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
    /// Returns the number of records in the table. You need to specify a table-level dbid. Unsupported 
    /// tag: &lt;udata&gt;&lt;/udata&gt;
    /// </summary>
    public class GetNumRecords : IQObject
    {
        private const string QUICKBASE_ACTION = "API_GetNumRecords";
        private readonly Payload.Payload _getNumRecordsPayload;
        private readonly IQUri _uri;

        /// <summary>
        /// Initializes a new instance of the com.intuit.quickbase.API_GetNumRecords class.
        /// </summary>
        /// <param name="ticket">Supply auth ticket for application access. See com.intuit.quickbase.API_Authenticate class to obtain a ticket.</param>
        /// <param name="appToken">Supply application token that is assigned to your QuickBase Application. See QuickBase Online help to obtain an application token.</param>
        /// <param name="accountDomain"></param>
        /// <param name="dbid">Supply table-level dbid.</param>
        public GetNumRecords(string ticket, string appToken, string accountDomain, string dbid)
        {
            this._getNumRecordsPayload = new GetNumRecordsPayload();
            this._getNumRecordsPayload = new ApplicationTicket(this._getNumRecordsPayload, ticket);
            this._getNumRecordsPayload = new ApplicationToken(this._getNumRecordsPayload, appToken);
            this._getNumRecordsPayload = new WrapPayload(this._getNumRecordsPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._getNumRecordsPayload.GetXmlPayload();
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