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
        public static Boolean HastAtLeast<TSource>(this IEnumerable<TSource> source
            , Int32 count)
            => ((source.Take(count).Count() == count));

        /// <summary>
        /// Checks if a list has at least one item.
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <param name="source">the original list</param>
        /// <returns>if a list has at least one item</returns>
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
        public static Boolean HastAtLeastItems<TSource>(this IEnumerable<TSource> source
            , Func<TSource, Boolean> predicate
            , Int32 count)
            => (source.Where(predicate).HastAtLeast(count));

        /// <summary>
        /// Checks if a list has at least one item that matches the predicate.
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <param name="source">the original list</param>
        /// <param name="predicate">the function delegate to the evaluator</param>
        /// <returns>if a list has at least one item that matches the predicate</returns>
        public static Boolean HasItemsWhere<TSource>(this IEnumerable<TSource> source
            , Func<TSource, Boolean> predicate)
            => (source.Any(predicate));
    }
}