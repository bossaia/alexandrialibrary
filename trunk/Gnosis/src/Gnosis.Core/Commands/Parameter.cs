﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core.Iso;

namespace Gnosis.Core.Commands
{
    public class Parameter
        : IParameter
    {
        public Parameter(string name, object value, bool isMultiValue)
        {
            this.name = name;
            this.value = value;
            this.isMultiValue = isMultiValue;
        }

        public Parameter(string name)
            : this(name, null, false)
        {
        }

        public Parameter(string name, IEntity entity)
            : this(name, entity.Id.ToString(), false)
        {
        }

        public Parameter(string name, IValue value)
            : this(name, value.Id.ToString(), false)
        {
        }

        public Parameter(string name, ILanguage language)
            : this(name, language.ToString(), false)
        {
        }

        public Parameter(string name, ICountry country)
            : this(name, country.ToString(), false)
        {
        }

        public Parameter(string name, Guid value)
            : this(name, value.ToString(), false)
        {
        }

        public Parameter(string name, bool value)
            : this(name, value ? 1 : 0, false)
        {
        }

        public Parameter(string name, Uri value)
            : this(name, value.ToString(), false)
        {
        }

        public Parameter(string name, DateTime value)
            : this(name, value.ToString("s"), false)
        {
        }

        public Parameter(string name, TimeSpan value)
            : this(name, value.Ticks, false)
        {
        }

        public Parameter(string name, IEnumerable<string> values)
            : this(name, values.ToNamesString(), true)
        {
        }

        public Parameter(string name, IEnumerable<ILanguage> languages)
            : this(name, languages.ToNamesString(), true)
        {
        }

        public Parameter(string name, IEnumerable<ICountry> countries)
            : this(name, countries.ToNamesString(), true)
        {
        }

        private readonly string name;
        private readonly object value;
        private readonly bool isMultiValue;

        public string Name
        {
            get { return name; }
        }

        public object Value
        {
            get { return value; }
        }
    }
}