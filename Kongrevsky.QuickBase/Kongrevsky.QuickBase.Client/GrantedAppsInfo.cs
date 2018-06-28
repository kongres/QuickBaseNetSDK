/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Client
{
    using System.Collections.Generic;

    public class GrantedAppsInfo : GrantedInfo
    {
        private readonly List<GrantedTablesInfo> _grantedTables;

        public GrantedAppsInfo(string name, string dbid)
            : base(name, dbid)
        {
            this._grantedTables = new List<GrantedTablesInfo>();
        }

        public void AddTable(string name, string dbid)
        {
            this._grantedTables.Add(new GrantedTablesInfo(name, dbid));
        }

        public List<GrantedTablesInfo> GrantedTables
        {
            get
            {
                return this._grantedTables;
            }
        }
    }
}
