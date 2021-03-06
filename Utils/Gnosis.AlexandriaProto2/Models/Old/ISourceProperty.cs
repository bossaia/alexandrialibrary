﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models
{
    public interface ISourceProperty
    {
        Guid Id { get; }
        ISource Source { get; }
        string Name { get; }
        Type BaseType { get; }
        object Default { get; }
        object Value { get; set; }
        string ValueHash { get; }
        string ValueMetaphone { get; }
        bool IsValid(object value);
    }
}
