﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Ietf;

namespace Gnosis.Core.Atom
{
    public class AtomSummary
        : AtomTextConstruct, IAtomSummary
    {
        public AtomSummary(Uri baseId, ILanguageTag lang, IEnumerable<IAtomExtension> extensions, string text, AtomTextType type)
            : base(baseId, lang, extensions, text, type)
        {
        }

        public override string ToString()
        {
            return ToString("summary");
        }
    }
}