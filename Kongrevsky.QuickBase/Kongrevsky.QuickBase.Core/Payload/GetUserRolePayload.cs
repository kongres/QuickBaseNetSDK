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

    internal class GetUserRolePayload : Payload
    {
        private string _userId;

        internal GetUserRolePayload(string userId)
        {
            UserId = userId;
        }

        private string UserId
        {
            get { return this._userId; }
            set
            {
                if (value == null) throw new ArgumentNullException("userId");
                if (value.Trim() == String.Empty) throw new ArgumentException("userId");
                this._userId = value;
            }
        }

        internal override string GetXmlPayload()
        {
            return new XElement("userid", UserId).ToString();
        }
    }
}
