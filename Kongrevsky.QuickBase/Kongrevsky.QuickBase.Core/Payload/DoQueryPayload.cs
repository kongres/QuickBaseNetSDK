/*
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

    internal class DoQueryPayload : Payload
    {
        private readonly string _query;
        private readonly int _qid;
        private readonly string _qName;
        private readonly string _cList;
        private readonly string _sList;
        private string _options;
        private readonly bool _fmt;

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

            internal string CList { get; private set; }
            internal Builder SetCList(string val)
            {
                CList = val;
                return this;
            }

            internal string SList { get; private set; }
            internal Builder SetSList(string val)
            {
                SList = val;
                return this;
            }

            internal bool Fmt { get; private set; }
            internal Builder SetFmt(bool val)
            {
                Fmt = val;
                return this;
            }

            internal string Options { get; private set; }
            internal Builder SetOptions(string val)
            {
                Options = val;
                return this;
            }

            internal DoQueryPayload Build()
            {
                return new DoQueryPayload(this);
            }
        }

        private DoQueryPayload(Builder builder)
        {
            this._query = builder.Query;
            this._qid = builder.Qid;
            this._qName = builder.QName;
            this._cList = builder.CList;
            this._sList = builder.SList;
            this._options = builder.Options;
            this._fmt = builder.Fmt;
        }

        public string Options
        {
            get { return this._options; }
            set { this._options = value; }
        }

        internal override string GetXmlPayload()
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this._query)) sb.Append(new XElement("query", this._query));
            if (this._qid > 0) sb.Append(new XElement("qid", this._qid));
            if (!string.IsNullOrEmpty(this._qName)) sb.Append(new XElement("qname", this._qName));
            if (!string.IsNullOrEmpty(this._cList)) sb.Append(new XElement("clist", this._cList));
            if (!string.IsNullOrEmpty(this._sList)) sb.Append(new XElement("slist", this._sList));
            if (!string.IsNullOrEmpty(this._options)) sb.Append(new XElement("options", this._options));
            sb.Append(new XElement("fmt", "structured"));
            return sb.ToString();
        }
    }
}
