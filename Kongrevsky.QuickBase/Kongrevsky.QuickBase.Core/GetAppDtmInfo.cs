/*
 * Copyright © 2013 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core
{
    using System;
    using System.Collections.Generic;
    using System.Xml.XPath;
    using Kongrevsky.QuickBase.Core.Uri;

    public class GetAppDtmInfo : IQGetObject
    {
        private const string QUICKBASE_ACTION = "API_GetAppDTMInfo";
        private readonly Dictionary<string, string> _map = new Dictionary<string, string>();
        private readonly IQUri _uri;

        public GetAppDtmInfo(string accountDomain, string dbid)
        {
            AddParameter("act=", Action);
            AddParameter("dbid=", dbid);
            this._uri = new QUriMain(accountDomain);
        }

        public void AddParameter(string name, string value)
        {
            this._map.Add(name, value);
        }

        public System.Uri Uri
        {
            get
            {
                var uriParams = String.Empty;
                foreach(var param in this._map)
                {
                    uriParams += param.Key + param.Value + "&";

                }
                return new System.Uri(this._uri.GetQUri() + "?" + uriParams.TrimEnd('&'));
            }
        }

        public string Action
        {
            get
            {
                return QUICKBASE_ACTION;
            }
        }

        public XPathDocument Get()
        {
            return new Http().Get(this);
        }
    }
}
