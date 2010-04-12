using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface ICommand
    {
        ITable Table { get; }
        object Id { get; }
        IMap<string, object> Changes { get; }
        ICommand Parent { get; }
        void SetResult(object result);
    }
}
