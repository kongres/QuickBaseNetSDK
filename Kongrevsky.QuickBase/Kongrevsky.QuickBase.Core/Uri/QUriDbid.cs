﻿/*
 * Copyright © 2013 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core.Uri
{
    using System;

    internal class QUriDbid : QUri
    {
        private string _dbid;
        private string _accountDomain;

        internal QUriDbid(string accountDomain, string dbid)
        {
            AccountDomain = accountDomain;
            Dbid = dbid;
        }

        private string AccountDomain
        {
            get { return this._accountDomain; }
            set
            {
                if (value == null) throw new ArgumentNullException("accountDomain");
                if (value.Trim() == String.Empty) throw new ArgumentException("accountDomain");
                this._accountDomain = value;
            }
        }

        private string Dbid
        {
            get { return this._dbid; }
            set
            {
                if (value == null) throw new ArgumentNullException("dbid");
                if (value.Trim() == String.Empty) throw new ArgumentException("dbid");
                this._dbid = value;
            }
        }

        public override System.Uri GetQUri()
        {
            return new System.Uri(BaseUri(AccountDomain) + ((Dbid.Trim().Length > 0) ? Dbid : String.Empty));
        }
    }
}