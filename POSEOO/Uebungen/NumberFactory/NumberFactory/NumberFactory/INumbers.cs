using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberFactory
{
    /// <summary>
    /// This interface defines the behavior and properties of a number collection.
    /// </summary>
    public interface INumbers : IEnumerable<long>
    {
        /// <summary>
        /// This indexer returns the number at the corresponding position.
        /// </summary>
        /// <param name="index">Index in the range 0 to Length - 1</param>
        /// <returns>The number at the position</returns>
        long this[int index] { get; }

        /// <summary>
        /// Returns the lower bound to the caller.
        /// </summary>
        long LowerBound { get; }

        /// <summary>
        /// Returns the upper bound to the caller.
        /// </summary>
        long UpperBound { get; }

        /// <summary>
        /// Returns the number of elements to the caller.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Returns the enumerator to the caller.
        /// </summary>
        /// <returns>Enumerator with which the collection can be traversed.</returns>
        IEnumerator<long> GetEnumerator();
    }
}