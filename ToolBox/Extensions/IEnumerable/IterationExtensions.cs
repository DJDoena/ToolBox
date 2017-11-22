namespace DoenaSoft.ToolBox.Extensions
{
    using System;
    using System.Collections.Generic;

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
    }
}