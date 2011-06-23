﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Core.Queries
{
    public interface IQuery<T>
        where T : IEntity
    {
        IEnumerable<T> Execute();
    }
}
