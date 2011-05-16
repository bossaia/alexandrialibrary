﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Repositories
{
    public interface ISearch
    {
        IEnumerable<KeyValuePair<string, object>> Parameters { get; }
        string GetWhereClause();
    }

    public interface ISearch<T> : ISearch
    {
    }
}
