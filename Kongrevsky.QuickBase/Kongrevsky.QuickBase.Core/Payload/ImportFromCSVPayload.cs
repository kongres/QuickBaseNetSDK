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

    internal class ImportFromCSVPayload : Payload
    {
        private readonly string _recordsCsv;
        private readonly string _cList;
        private readonly bool _skipFirst;
        private readonly bool _timeInUtc;

        internal class Builder
        {
            internal Builder(string recordsCsv)
            {
                RecordsCsv = recordsCsv;
            }

            internal string RecordsCsv { get; set; }

            internal string CList { get; private set; }
            internal Builder SetCList(string val)
            {
                CList = val;
                return this;
            }

            internal bool SkipFirst { get; private set; }
            internal Builder SetSkipFirst(bool val)
            {
                SkipFirst = val;
                return this;
            }

            internal bool TimeInUtc { get; private set; }
            internal Builder SetTimeInUtc(bool val)
            {
                TimeInUtc = val;
                return this;
            }

            internal ImportFromCSVPayload Build()
            {
                return new ImportFromCSVPayload(this);
            }
        }

        private ImportFromCSVPayload(Builder builder)
        {
            this._recordsCsv = builder.RecordsCsv;
            this._cList = builder.CList;
            this._skipFirst = builder.SkipFirst;
            this._timeInUtc = builder.TimeInUtc;
        }

        internal override string GetXmlPayload()
        {
            var sb = new StringBuilder();
            sb.Append(new XElement("records_csv", new XCData(this._recordsCsv)));
            if (!string.IsNullOrEmpty(this._cList)) sb.Append(new XElement("clist", this._cList));
            if (this._skipFirst) sb.Append(new XElement("skipfirst", 1));
            if (this._timeInUtc) sb.Append(new XElement("msInUTC", 1));
            return sb.ToString();
        }
    }
}
