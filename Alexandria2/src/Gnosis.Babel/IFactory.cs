using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IFactory<T>
    {
        T Create();
        T Create(ITuple data);
    }

    public interface IFactory<T, A>
        where T : IEntity<A>
    {
        T Create();
        T Create(ITuple<A> data);
    }

    public interface IFactory<T, A, B>
        where T : IEntity<A, B>
    {
        T Create();
        T Create(ITuple<A, B> data);
    }
}
