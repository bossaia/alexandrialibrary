using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Babel
{
    public interface ISet<T> :
        IEnumerable<T>,
        IEquatable<ISet<T>>
    {
        /// <summary>
        /// Get a set of all members of this set OR the given set (or both)
        /// </summary>
        /// <param name="set">A set of T</param>
        /// <returns>A set of T</returns>
        ISet<T> Union(ISet<T> set);
        
        /// <summary>
        /// Get a set of all members of this set AND the given set
        /// </summary>
        /// <param name="set">A set of T</param>
        /// <returns>A set of T</returns>
        ISet<T> Intersection(ISet<T> set);

        /// <summary>
        /// Get a set of all members of this set NOT in the given set
        /// </summary>
        /// <param name="set">A set of T</param>
        /// <returns>A set of T</returns>
        ISet<T> Complement(ISet<T> set);

        /// <summary>
        /// Get a set of all members of this set NOT in the given set and all members of the given set NOT in this set
        /// </summary>
        /// <param name="set">A set of T</param>
        /// <returns>A set of T</returns>
        ISet<T> SymmetricDifference(ISet<T> set);

        /// <summary>
        /// Get a set of ordered pairs of all possible combinations of this set and the given set
        /// </summary>
        /// <param name="set">A set of T</param>
        /// <returns>A set of ordered pairs of T</returns>
        ISet<ITuple<T, T>> CartesianProduct(ISet<T> set);

        /// <summary>
        /// Get a set of all subsets of this set
        /// </summary>
        /// <returns>A set of sets</returns>
        ISet<ISet<T>> PowerSet();

        /// <summary>
        /// Get a set of all members of this set that satisfy the given predicate
        /// </summary>
        /// <param name="predicate">A predicate of T</param>
        /// <returns>A set of T</returns>
        ISet<T> Subset(IExpression<T, bool> predicate);
    }
}
