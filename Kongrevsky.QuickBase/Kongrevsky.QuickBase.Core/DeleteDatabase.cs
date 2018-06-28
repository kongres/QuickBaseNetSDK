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
    /// If you have application administration rights, you can use this call to delete 
    /// either a child table or the entire application, depending on the dbid you supply. 
    /// If you supply the application-level dbid you delete the application. If you 
    /// supply a table-level dbid you delete the table. Unsupported tag: &lt;udata&gt;&lt;/udata&gt;
    /// </summary>
    public class DeleteDatabase : IQObject
    {
        private const string QUICKBASE_ACTION = "API_DeleteDatabase";
        private readonly Payload.Payload _deleteDatabasePayload;
        private readonly IQUri _uri;

        /// <summary>
        /// Initializes a new instance of the com.intuit.quickbase.API_DeleteDatabase class.
        /// </summary>
        /// <param name="ticket">Supply auth ticket for application access. See com.intuit.quickbase.API_Authenticate class to obtain a ticket.</param>
        /// <param name="appToken">Supply application token that is assigned to your QuickBase Application. See QuickBase Online help to obtain an application token.</param>
        /// <param name="accountDomain"></param>
        /// <param name="dbid">Supply application-level or table-level dbid.</param>
        public DeleteDatabase(string ticket, string appToken, string accountDomain, string dbid)
        {
            this._deleteDatabasePayload = new DeleteDatabasePayload();
            this._deleteDatabasePayload = new ApplicationTicket(this._deleteDatabasePayload, ticket);
            this._deleteDatabasePayload = new ApplicationToken(this._deleteDatabasePayload, appToken);
            this._deleteDatabasePayload = new WrapPayload(this._deleteDatabasePayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._deleteDatabasePayload.GetXmlPayload();
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