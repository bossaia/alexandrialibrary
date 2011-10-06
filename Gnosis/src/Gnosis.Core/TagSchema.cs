﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core
{
    public class TagSchema
        : ITagSchema
    {
        public TagSchema(long id, string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            this.id = id;
            this.name = name;
        }

        private readonly long id;
        private readonly string name;

        public long Id
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
        }

        public static readonly ITagSchema Default = new TagSchema(1, "Default");
    }
}
