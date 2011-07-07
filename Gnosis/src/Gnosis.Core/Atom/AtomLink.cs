﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;
using Gnosis.Core.W3c;

namespace Gnosis.Core.Atom
{
    public class AtomLink
        : AtomCommon, IAtomLink
    {
        public AtomLink(Uri baseId, ILanguageTag lang, Uri href, string rel, IMediaType type, ILanguageTag hrefLang, string title, int length)
            : base(baseId, lang)
        {
            this.href = href;
            this.rel = rel;
            this.type = type;
            this.hrefLang = hrefLang;
            this.title = title;
            this.length = length;
        }

        private readonly Uri href;
        private readonly string rel;
        private readonly IMediaType type;
        private readonly ILanguageTag hrefLang;
        private readonly string title;
        private readonly int length;

        #region IAtomLink Members

        public Uri Href
        {
            get { return href; }
        }

        public string Rel
        {
            get { return rel; }
        }

        public IMediaType Type
        {
            get { return type; }
        }

        public ILanguageTag HrefLang
        {
            get { return hrefLang; }
        }

        public string Title
        {
            get { return title; }
        }

        public int Length
        {
            get { return length; }
        }

        #endregion
    }
}
