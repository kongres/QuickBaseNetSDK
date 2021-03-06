﻿/*
 * Copyright © 2010 Intuit Inc. All rights reserved.
 * All rights reserved. This program and the accompanying materials
 * are made available under the terms of the Eclipse Public License v1.0
 * which accompanies this distribution, and is available at
 * http://www.opensource.org/licenses/eclipse-1.0.php
 */

namespace Kongrevsky.QuickBase.Core.Payload
{
    using System.Text;
    using System.Xml.Linq;

    internal class PurgeRecordsPayload : Payload
    {
        private readonly string _query;
        private readonly int _qid;
        private readonly string _qname;

        internal class Builder
        {
            internal string Query { get; private set; }
            internal Builder SetQuery(string val)
            {
                Query = val;
                return this;
            }

            internal int Qid { get; private set; }
            internal Builder SetQid(int val)
            {
                Qid = val;
                return this;
            }

            internal string QName { get; private set; }
            internal Builder SetQName(string val)
            {
                QName = val;
                return this;
            }

            internal PurgeRecordsPayload Build()
            {
                return new PurgeRecordsPayload(this);
            }
        }

        private PurgeRecordsPayload(Builder builder)
        {
            this._query = builder.Query;
            this._qid = builder.Qid;
            this._qname = builder.QName;
        }

        internal override string GetXmlPayload()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this._query)) sb.Append(new XElement("query", this._query));
            if (this._qid > 0) sb.Append(new XElement("qid", this._qid));
            if (!string.IsNullOrEmpty(this._qname)) sb.Append(new XElement("qname", this._qname));
            return sb.ToString();
        }
    }
}
