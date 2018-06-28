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
    using System.Text;
    using System.Xml.Linq;

    internal class AddUserToRolePayload : Payload
    {
        private string _userId;
        private int _roleId;

        internal AddUserToRolePayload(string userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
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

        private int RoleId
        {
            get { return this._roleId; }
            set
            {
                if (value < 0) throw new ArgumentException("roleId");
                this._roleId = value;
            }
        }

        internal override string GetXmlPayload()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(new XElement("userid", UserId));
            sb.Append(new XElement("roleid", RoleId));
            return sb.ToString();
        }
    }
}
