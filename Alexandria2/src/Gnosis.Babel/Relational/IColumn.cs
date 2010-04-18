using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel.Relational
{
    public interface IColumn
    {
        ITable Table { get; }
        string Name { get; }
        Type DataType { get; }
        bool Unicode { get; }
        bool VariableLength { get; }
        int Length { get; }
        int Precision { get; }
        int Scale { get; }
        IExpression Default { get; }
    }
}
