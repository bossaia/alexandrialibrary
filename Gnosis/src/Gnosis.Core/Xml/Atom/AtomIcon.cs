﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Xml.Atom
{
    public class AtomIcon
        : AtomCommon, IAtomIcon
    {
        public AtomIcon(INode parent, IQualifiedName name)
            : base(parent, name)
        {
        }

        public Uri Uri
        {
            get { return GetContentUri(); }
        }
    }
}
