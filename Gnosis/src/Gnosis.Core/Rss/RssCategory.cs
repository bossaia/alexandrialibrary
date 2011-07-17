﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Rss
{
    public class RssCategory
        : IRssCategory
    {
        public RssCategory(Uri domain, string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.domain = domain;
            this.name = name;
        }

        private readonly Uri domain;
        private readonly string name;

        #region IRssCategory Members

        public Uri Domain
        {
            get { return domain; }
        }

        public string Name
        {
            get { return name; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();

            xml.Append("<category");

            if (domain != null)
                xml.AppendFormat(" domain='{0}'", domain.ToXmlEscapedString());

            xml.Append(">");
            xml.AppendFormat("{0}</category>", name.ToXmlEscapedString());
            xml.AppendLine();

            return xml.ToString();
        }
    }
}