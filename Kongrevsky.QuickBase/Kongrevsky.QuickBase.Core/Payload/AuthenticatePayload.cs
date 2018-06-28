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

    internal class AuthenticatePayload : Payload
    {
        private string _username;
        private string _password;
        private int _hours;

        internal AuthenticatePayload(string username, string password, int hours)
            : this(username, password)
        {
            Hours = hours;
        }

        internal AuthenticatePayload(string username, string password)
        {
            Username = username;
            Password = password;
        }

        private string Username
        {
            get { return this._username; }
            set
            {
                if (value == null) throw new ArgumentNullException("username");
                if (value.Trim() == String.Empty) throw new ArgumentException("username");
                this._username = value;
            }
        }

        private string Password
        {
            get { return this._password; }
            set
            {
                if (value == null) throw new ArgumentNullException("password");
                if (value.Trim() == String.Empty) throw new ArgumentException("password");
                this._password = value;
            }
        }

        private int Hours
        {
            get { return this._hours; }
            set
            {
                if (value < 0) throw new ArgumentException("hours");
                this._hours = value;
            }
        }

        internal override string GetXmlPayload()
        {
            var sb = new StringBuilder();
            sb.Append(new XElement("username", Username));
            sb.Append(new XElement("password", Password));
            if (Hours > 0) sb.Append(new XElement("hours", Hours));
            return sb.ToString();
        }
    }
}
