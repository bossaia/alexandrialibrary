﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public abstract class AtomPerson
        : AtomCommon, IAtomPerson
    {
        protected AtomPerson(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, string name, Uri uri, string email)
            : base(baseId, lang, extensions, namespaces, primaryNamespace)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.name = name;
            this.uri = uri;
            this.email = email;
        }

        private readonly string name;
        private readonly Uri uri;
        private readonly string email;

        protected string ToString(string tag)
        {
            var xml = new StringBuilder();

            AppendStartTag(xml, tag);

            xml.AppendFormat("<name>{0}</name>", Name);
            if (Uri != null)
                xml.AppendFormat("<uri>{0}</uri>", Uri.ToString().ToXmlEscapedString());
            if (Email != null)
                xml.AppendFormat("<email>{0}</email>", Email);

            AppendEndTag(xml, tag);

            return xml.ToString();
        }

        #region IAtomPerson Members

        public string Name
        {
            get { return name; }
        }

        public Uri Uri
        {
            get { return uri; }
        }

        public string Email
        {
            get { return email; }
        }

        #endregion
    }
}
