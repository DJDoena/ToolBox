namespace DoenaSoft.ToolBox.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary />
    public static class CountExtensions
    {
        /// <summary>
        /// Checks if a list has at least n items.
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <param name="source">the original list</param>
        /// <param name="count">the number of items that must be in the list</param>
        /// <returns>if a list has at least n items</returns>
        public static Boolean HastAtLeastItems<TSource>(this IEnumerable<TSource> source
            , Int32 count)
            => ((source.Take(count).Count() == count));

        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}" /> to check for emptiness</param>
        /// <returns>true if the source sequence contains any elements; otherwise, false</returns>
        public static Boolean HasItems<TSource>(this IEnumerable<TSource> source)
            => (source.Any());

        /// <summary>
        /// Checks if a list has at least n items that matches the predicate.
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <param name="source">the original list</param>
        /// <param name="predicate">the function delegate to the evaluator</param>
        /// <param name="count">the number of items that must match the predicate</param>
        /// <returns>if a list has at least n items that matches the predicate</returns>
        public static Boolean HastAtLeastItemsWhere<TSource>(this IEnumerable<TSource> source
            , Func<TSource, Boolean> predicate
            , Int32 count)
            => (source.Where(predicate).HastAtLeastItems(count));

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable`1 whose elements to apply the predicate to</param>
        /// <param name="predicate">A function to test each element for a condition</param>
        /// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false</returns>
        public static Boolean HasItemsWhere<TSource>(this IEnumerable<TSource> source
            , Func<TSource, Boolean> predicate)
            => (source.Any(predicate));

        /// <summary>
        /// Computes the sum of a sequence of <see cref="UInt32" /> values.
        /// </summary>
        /// <param name="source">A sequence of <see cref="UInt32" /> values to calculate the sum of</param>
        /// <returns>The sum of the values in the sequence</returns>
        public static UInt32 Sum(this IEnumerable<UInt32> source)
            => (source.Sum(i => i));

        /// <summary>
        /// Computes the sum of the sequence of <see cref="UInt32" /> values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">A sequence of values that are used to calculate a sum</param>
        /// <param name="selector">A transform function to apply to each element</param>
        /// <returns>The type of the elements of source</returns>
        public static UInt32 Sum<TSource>(this IEnumerable<TSource> source
            , Func<TSource, UInt32> selector)
        {
            if (source == null)
            {
                throw (new ArgumentNullException(nameof(source)));
            }
            else if (selector == null)
            {
                throw (new ArgumentNullException(nameof(selector)));
            }

            UInt32 sum = 0;

            foreach (TSource item in source)
            {
                sum = checked(sum + selector(item));
            }

            return (sum);
        }
    }
}