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
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.Linq;

    internal class FieldChoicesPayload : Payload
    {
        private int _fid;
        private List<string> _choices;

        internal FieldChoicesPayload(int fid, List<string> choices)
        {
            Fid = fid;
            Choices = choices;
        }

        private int Fid
        {
            get { return this._fid; }
            set
            {
                if (value < 1) throw new ArgumentException("fid");
                this._fid = value;
            }
        }

        private List<string> Choices
        {
            get { return this._choices; }
            set
            {
                if (value == null) throw new ArgumentNullException("choices");
                this._choices = value;
            }
        }

        internal override string GetXmlPayload()
        {
            var sb = new StringBuilder();
            sb.Append(new XElement("fid", Fid));
            foreach(var choice in Choices)
            {
                sb.Append(new XElement("choice", choice));
            }
            return sb.ToString();
        }
    }
}
