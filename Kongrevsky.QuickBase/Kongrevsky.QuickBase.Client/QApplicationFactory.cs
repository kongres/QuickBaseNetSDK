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

    internal class QApplicationFactory : QApplicationFactoryBase
    {
        private static QApplicationFactoryBase _instance;

        private QApplicationFactory() { }

        internal static QApplicationFactoryBase GetInstance()
        {
            if(_instance == null)
            {
                _instance = new QApplicationFactory();
            }
            return _instance;
        }

        internal override IQApplication CreateInstance(IQClient client, string applicationId, string token, List<string> loadTables = null)
        {
            var tableFactory = QTableFactory.GetInstance();
            return new QApplication(tableFactory, client, applicationId, token, loadTables);
        }
    }
}
