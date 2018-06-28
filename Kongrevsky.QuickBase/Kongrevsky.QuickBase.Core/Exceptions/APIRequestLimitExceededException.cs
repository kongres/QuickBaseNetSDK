﻿/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core.Exceptions
{
    using System;

    public class ApiRequestLimitExceededException : Exception
    {
        public ApiRequestLimitExceededException() { }

        public ApiRequestLimitExceededException(string message, DateTime waitUntil)
        {
            Message = message;
            WaitUntil = waitUntil;
        }

        public new string Message { get; private set; }

        public DateTime WaitUntil { get; private set; }
    }
}