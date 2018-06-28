/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core.Payload
{
    using System;
    using System.Xml.Linq;

    internal class GetUserInfoPayload : Payload
    {
        private string _email;

        internal GetUserInfoPayload(string email)
        {
            Email = email;
        }

        internal GetUserInfoPayload() { }

        private string Email
        {
            get { return this._email; }
            set
            {
                if (value == null) throw new ArgumentNullException("email");
                if (value.Trim() == String.Empty) throw new ArgumentException("email");
                this._email = value;
            }
        }

        internal override string GetXmlPayload()
        {
            return !String.IsNullOrEmpty(Email) ? new XElement("email", Email).ToString() : string.Empty;
        }
    }
}
