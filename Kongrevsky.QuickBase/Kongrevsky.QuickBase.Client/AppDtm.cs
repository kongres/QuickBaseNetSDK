﻿/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Client
{
    using System.Collections.Generic;

    public class AppDtm : Dtm
    {
        private readonly List<TableDtm> _tableDtm;

        public AppDtm(string dbid, long lastModifiedTime, long lastRecModTime, long requestTime, long requestNextAllowedTime)
            : base(dbid, lastModifiedTime, lastRecModTime)
        {
            RequestTime = requestTime;
            RequestNextAllowedTime = requestNextAllowedTime;
            this._tableDtm = new List<TableDtm>();
        }

        public long RequestTime { get; private set; }
        public long RequestNextAllowedTime { get; private set; }

        public void AddTable(string dbid, long lastModifiedTime, long lastRecModTime)
        {
            this._tableDtm.Add(new TableDtm(dbid, lastModifiedTime, lastRecModTime));
        }

        public List<TableDtm> Tables
        {
            get
            {
                return this._tableDtm;
            }
        }
    }
}
