﻿/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Client
{
    using Kongrevsky.QuickBase.Core;

    internal abstract class QColumnFactoryBase
    {
        internal abstract IQColumn CreateInstace(int columnId, string columnName, FieldType columnType, bool columnVirtual, bool columnLookup, bool isHidden);
    }
}
