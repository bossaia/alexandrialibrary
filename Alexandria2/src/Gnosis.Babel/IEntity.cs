using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface IEntity :
        INamed
    {
        IEnumerable<IElement> Elements { get; }
        IEnumerable<IKey> Keys { get; }
        IElement GetElement(string name);
        IKey GetKey(string name);
    }

    public interface IEntity<A>
        : IEntity,
        ITuple<A>
    {
    }

    public interface IEntity<A, B>
        : IEntity,
        ITuple<A, B>
    {
    }

    public interface IEntity<A, B, C>
        : IEntity,
        ITuple<A, B, C>
    {
    }

    public interface IEntity<A, B, C, D>
        : IEntity,
        ITuple<A, B, C, D>
    {
    }

    public interface IEntity<A, B, C, D, E>
        : IEntity,
        ITuple<A, B, C, D, E>
    {
    }

    public interface IEntity<A, B, C, D, E, F>
        : IEntity,
        ITuple<A, B, C, D, E, F>
    {
    }

    public interface IEntity<A, B, C, D, E, F, G>
        : IEntity,
        ITuple<A, B, C, D, E, F, G>
    {
    }

    public interface IEntity<A, B, C, D, E, F, G, H>
        : IEntity,
        ITuple<A, B, C, D, E, F, G, H>
    {
    }

    public interface IEntity<A, B, C, D, E, F, G, H, I>
        : IEntity,
        ITuple<A, B, C, D, E, F, G, H, I>
    {
    }
}
