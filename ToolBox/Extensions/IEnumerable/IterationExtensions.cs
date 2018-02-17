namespace DoenaSoft.ToolBox.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary />
    public static class IterationExtensions
    {
        /// <summary>
        /// Executes an action on each item of a list.
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <param name="source">the original list</param>
        /// <param name="action">the action to be executed on each item</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source
            , Action<TSource> action)
        {
            if (source == null)
            {
                throw (new ArgumentNullException(nameof(source)));
            }
            else if (action == null)
            {
                throw (new ArgumentNullException(nameof(action)));
            }

            foreach (TSource item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Ensures that a given <see cref="IEnumerable{T}"/> is never null.
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <param name="source">the original list</param>
        /// <returns>a valid object reference</returns>
        public static IEnumerable<TSource> EnsureNotNull<TSource>(this IEnumerable<TSource> source)
            => source ?? Enumerable.Empty<TSource>();

        /// <summary>
        /// Filters a sequence of values based on a predicate, then projects each element of a sequence into a new form.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <param name="source">A sequence of values to to filter and project.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains projected elements from the input sequence that satisfy the predicate.</returns>
        public static IEnumerable<TResult> SelectWhere<TSource, TResult>(this IEnumerable<TSource> source
            , Func<TSource, Boolean> predicate
            , Func<TSource, TResult> selector)
            => source.Where(predicate).Select(selector);
    }
}