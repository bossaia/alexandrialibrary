using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IValue :
        INamed
    {
        IEnumerable<IElement> Elements { get; }
        IElement GetElement(string name);
    }

    public interface IValue<A> :
        IValue,
        ITuple<A>
    {
    }

    public interface IValue<A, B> :
        IValue,
        ITuple<A, B>
    {
    }

    public interface IValue<A, B, C> :
        IValue,
        ITuple<A, B, C>
    {
    }

    public interface IValue<A, B, C, D> :
        IValue,
        ITuple<A, B, C, D>
    {
    }

    public interface IValue<A, B, C, D, E> :
        IValue,
        ITuple<A, B, C, D, E>
    {
    }

    public interface IValue<A, B, C, D, E, F> :
        IValue,
        ITuple<A, B, C, D, E, F>
    {
    }

    public interface IValue<A, B, C, D, E, F, G> :
        IValue,
        ITuple<A, B, C, D, E, F, G>
    {
    }

    public interface IValue<A, B, C, D, E, F, G, H> :
        IValue,
        ITuple<A, B, C, D, E, F, G, H>
    {
    }

    public interface IValue<A, B, C, D, E, F, G, H, I> :
        IValue,
        ITuple<A, B, C, D, E, F, G, H, I>
    {
    }

    public interface IValue<A, B, C, D, E, F, G, H, I, J> :
        IValue,
        ITuple<A, B, C, D, E, F, G, H, I, J>
    {
    }
}
