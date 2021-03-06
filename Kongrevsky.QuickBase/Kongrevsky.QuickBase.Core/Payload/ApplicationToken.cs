﻿/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core.Payload
{
    using System.Xml.Linq;

    internal class ApplicationToken : PayloadDecorator
    {
        internal ApplicationToken(Payload payload, string token)
        {
            Payload = payload;
            Token = token;
        }

        private string Token { get; set; }

        internal override string GetXmlPayload()
        {
            return Payload.GetXmlPayload() +
                   (!string.IsNullOrEmpty(Token) ? new XElement("apptoken", Token).ToString() : string.Empty);
        }
    }
}