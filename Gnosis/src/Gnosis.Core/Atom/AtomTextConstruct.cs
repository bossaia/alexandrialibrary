﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public abstract class AtomTextConstruct
        : AtomCommon, IAtomTextConstruct
    {
        protected AtomTextConstruct(Uri baseId, ILanguageTag lang, string text)
            : base(baseId, lang)
        {
            this.text = text;
        }

        protected AtomTextConstruct(Uri baseId, ILanguageTag lang, string text, AtomTextType type)
            : base(baseId, lang)
        {
            this.text = text;
            this.type = type;
        }

        private readonly string text;
        private readonly AtomTextType type = AtomTextType.Text;

        #region IAtomTextConstruct Members

        public string Text
        {
            get { return text; }
        }

        public AtomTextType Type
        {
            get { return type; }
        }

        #endregion
    }
}
