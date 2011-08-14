﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomContent
        : AtomTextConstruct, IAtomContent
    {
        public AtomContent(Uri baseId, ILanguageTag lang, IEnumerable<IXmlExtension> extensions, IEnumerable<IXmlNamespace> namespaces, IXmlNamespace primaryNamespace, string text, AtomTextType type, IMediaType mediaType, Uri src)
            : base(baseId, lang, extensions, namespaces, primaryNamespace, text, type)
        {
            this.mediaType = mediaType;
            this.src = src;
        }

        private readonly IMediaType mediaType;
        private readonly Uri src;

        #region IAtomContent Members

        public IMediaType MediaType
        {
            get { return mediaType; }
        }

        public Uri Src
        {
            get { return src; }
        }

        #endregion

        public override string ToString()
        {
            var xml = new StringBuilder();
            var attributes = new Dictionary<string, string>();
            string srcEscaped = src != null ? src.ToString().ToXmlEscapedString() : null;

            attributes.AddIfNotNull("src", srcEscaped);

            if (mediaType != null)
                attributes.Add("type", mediaType.ToString());
            else
                attributes.Add("type", Type.ToString().ToLower());

            xml.Append("<content");

            AppendTagAttributes(xml, attributes, new List<IXmlNamespace>());

            xml.AppendFormat(">{0}</content>", Text ?? string.Empty);

            return xml.ToString();
        }
    }
}
