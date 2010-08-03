using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IExpression<T>
    {
        T Evaluate(T input);
    }

    public interface IExpression<Input, Output>
    {
        Output Evaluate(Input input);
    }
}
