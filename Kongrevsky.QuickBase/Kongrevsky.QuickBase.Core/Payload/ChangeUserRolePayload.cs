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

    internal class ChangeUserRolePayload : Payload
    {
        private string _userId;
        private int _roleId;
        private int _newRoleId;

        internal ChangeUserRolePayload(string userId, int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        internal ChangeUserRolePayload(string userId, int roleId, int newRoldId)
            : this(userId, roleId)
        {
            NewRoleId = newRoldId;
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
                // value of 0 okay
                if (value < 0) throw new ArgumentException("roleId");
                this._roleId = value;
            }
        }
        
        private int NewRoleId
        {
            get { return this._newRoleId; }
            set
            {
                if (value < 1) throw new ArgumentException("newRoleId");
                this._newRoleId = value;
            }
        }

        internal override string GetXmlPayload()
        {
            var sb = new StringBuilder();
            sb.Append(new XElement("userid", UserId));
            sb.Append(new XElement("roleid", RoleId));
            if (NewRoleId > 0)
                sb.Append(new XElement("newRoleid", NewRoleId));
            else
                sb.Append(new XElement("newRoleid"));
            return sb.ToString();
        }
    }
}
