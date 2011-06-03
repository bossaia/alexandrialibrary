﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Alexandria.Models.HashCodes
{
    public class NameHash
        : ValueBase, IHashCode
    {
        public NameHash(string value)
            : this(Guid.NewGuid(), value)
        {
        }

        public NameHash(Guid id, string value)
            : base(id)
        {
            this.value = value;
        }

        private readonly string value;

        public Uri Scheme
        {
            get { return Namespace; }
        }

        public string Value
        {
            get { return value; }
        }

        public static Uri Namespace = new Uri("http://alxlib.com/domain/1/hash-schemes/double-metaphone/1");

        public static IHashCode Create(string originalString)
        {
            if (string.IsNullOrEmpty(originalString))
                return null;

            return new NameHash(originalString.AsNameHash());
        }
    }
}
