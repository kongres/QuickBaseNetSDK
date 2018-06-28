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

    internal class DeleteFieldPayload : Payload
    {
        private int _fid;

        internal DeleteFieldPayload(int fid)
        {
            Fid = fid;
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

        internal override string GetXmlPayload()
        {
            return new XElement("fid", Fid).ToString();
        }
    }
}
