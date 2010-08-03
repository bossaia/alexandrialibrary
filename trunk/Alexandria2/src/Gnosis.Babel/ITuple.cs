using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface ITuple
        : IEnumerable<object>,
        IEquatable<ITuple>
    {
        int Length { get; }
        Type GetType(int index);
        object GetValue(int index);
        T GetValue<T>(int index);
    }

    public interface ITuple<A>
        : ITuple,
        IEquatable<ITuple<A>>
    {
        A Item1 { get; }
    }

    public interface ITuple<A, B>
        : ITuple,
        IEquatable<ITuple<A, B>>
    {
        A Item1 { get; }
        B Item2 { get; }
    }

    public interface ITuple<A, B, C>
        : ITuple,
        IEquatable<ITuple<A, B, C>>
    {
        A Item1 { get; }
        B Item2 { get; }
        C Item3 { get; }
    }

    public interface ITuple<A, B, C, D>
        : ITuple,
        IEquatable<ITuple<A, B, C, D>>
    {
        A Item1 { get; }
        B Item2 { get; }
        C Item3 { get; }
        D Item4 { get; }
    }

    public interface ITuple<A, B, C, D, E>
        : ITuple,
        IEquatable<ITuple<A, B, C, D, E>>
    {
        A Item1 { get; }
        B Item2 { get; }
        C Item3 { get; }
        D Item4 { get; }
        E Item5 { get; }
    }

    public interface ITuple<A, B, C, D, E, F>
        : ITuple,
        IEquatable<ITuple<A, B, C, D, E, F>>
    {
        A Item1 { get; }
        B Item2 { get; }
        C Item3 { get; }
        D Item4 { get; }
        E Item5 { get; }
        F Item6 { get; }
    }

    public interface ITuple<A, B, C, D, E, F, G>
        : ITuple,
        IEquatable<ITuple<A, B, C, D, E, F, G>>
    {
        A Item1 { get; }
        B Item2 { get; }
        C Item3 { get; }
        D Item4 { get; }
        E Item5 { get; }
        F Item6 { get; }
        G Item7 { get; }
    }

    public interface ITuple<A, B, C, D, E, F, G, H>
        : ITuple,
        IEquatable<ITuple<A, B, C, D, E, F, G, H>>
    {
        A Item1 { get; }
        B Item2 { get; }
        C Item3 { get; }
        D Item4 { get; }
        E Item5 { get; }
        F Item6 { get; }
        G Item7 { get; }
        H Item8 { get; }
    }

    public interface ITuple<A, B, C, D, E, F, G, H, I>
        : ITuple,
        IEquatable<ITuple<A, B, C, D, E, F, G, H, I>>
    {
        A Item1 { get; }
        B Item2 { get; }
        C Item3 { get; }
        D Item4 { get; }
        E Item5 { get; }
        F Item6 { get; }
        G Item7 { get; }
        H Item8 { get; }
        I Item9 { get; }
    }

    public interface ITuple<A, B, C, D, E, F, G, H, I, J>
        : ITuple,
        IEquatable<ITuple<A, B, C, D, E, F, G, H, I, J>>
    {
        A Item1 { get; }
        B Item2 { get; }
        C Item3 { get; }
        D Item4 { get; }
        E Item5 { get; }
        F Item6 { get; }
        G Item7 { get; }
        H Item8 { get; }
        I Item9 { get; }
        J Item10 { get; }
    }
}
