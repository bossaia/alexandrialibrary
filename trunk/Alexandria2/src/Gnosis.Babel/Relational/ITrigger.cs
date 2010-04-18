using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Core;

namespace Gnosis.Babel.Relational
{
    public interface ITrigger
    {
        bool Temporary { get; }
        IDatabase Database { get; }
        string Name { get; }
        TriggerType Type { get; }
        ISet<IColumn> Of { get; }
        ISource On { get; }
        bool ForEachRow { get; }
        IExpression When { get; }
        ISet<IStatement> Actions { get; } 
    }
}
