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
    /// If you have application administration rights, you can use this call to delete a 
    /// table field by specifying the field id (fid) in the &lt;fid&gt; param. You have to 
    /// use a table-level dbid. If you use an application level dbid you’ll get an 
    /// error 31, No such field. This call will delete the field for all records, whether 
    /// there is data in it or not, and there will be no warning message. Unsupported tag: 
    /// &lt;udata&gt;&lt;/udata&gt;
    /// </summary>
    public class DeleteField : IQObject
    {
        private const string QUICKBASE_ACTION = "API_DeleteField";
        private readonly Payload.Payload _deleteFieldPayload;
        private readonly IQUri _uri;

        /// <summary>
        /// Initializes a new instance of the com.intuit.quickbase.API_DeleteField class.
        /// </summary>
        /// <param name="ticket">Supply auth ticket for application access. See com.intuit.quickbase.API_Authenticate class to obtain a ticket.</param>
        /// <param name="appToken">Supply application token that is assigned to your QuickBase Application. See QuickBase Online help to obtain an application token.</param>
        /// <param name="accountDomain"></param>
        /// <param name="dbid">Supply table-level dbid.</param>
        /// <param name="fid">Supply a column object.</param>
        public DeleteField(string ticket, string appToken, string accountDomain, string dbid, int fid)
        {
            this._deleteFieldPayload = new DeleteFieldPayload(fid);
            this._deleteFieldPayload = new ApplicationTicket(this._deleteFieldPayload, ticket);
            this._deleteFieldPayload = new ApplicationToken(this._deleteFieldPayload, appToken);
            this._deleteFieldPayload = new WrapPayload(this._deleteFieldPayload);
            this._uri = new QUriDbid(accountDomain, dbid);
        }

        public string XmlPayload
        {
            get
            {
                return this._deleteFieldPayload.GetXmlPayload();
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