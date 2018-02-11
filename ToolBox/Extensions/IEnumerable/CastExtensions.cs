namespace DoenaSoft.ToolBox.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary />
    public static class CastExtensions
    {
        /// <summary>
        /// Returns a list of objects that could be cast to the given type.
        /// <remarks><see cref="CultureInfo.InvariantCulture"/></remarks>
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <typeparam name="TResult">the type of the cast</typeparam>
        /// <param name="source">the original list</param>
        /// <returns>a list of successfully casted objects</returns>
        public static IEnumerable<TResult> TryCast<TSource, TResult>(this IEnumerable<TSource> source)
            => (source.OfType<TResult>());

        /// <summary>
        /// Tries to cast the objects of a given list to a given type and returns the successfully and unsuccessfully casted objects in two lists.
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <typeparam name="TResult">the type of the cast</typeparam>
        /// <param name="source">the original list</param>
        /// <param name="castedList">a list of successfully casted objects</param>
        /// <param name="uncastedList">a list of unsuccessfully casted objects</param>
        /// <param name="noNullsInUncastedList">determines if the uncastedList can contain null values</param>
        public static void TryCast<TSource, TResult>(this IEnumerable<TSource> source
            , out IEnumerable<TResult> castedList
            , out IEnumerable<TSource> uncastedList
            , Boolean noNullsInUncastedList = false)
        {
            castedList = source.TryCast<TSource, TResult>();

            uncastedList = source.GetUncastable<TSource, TResult>(noNullsInUncastedList);
        }

        /// <summary>
        /// Returns a list of objects that cannot be cast to the given type.
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <typeparam name="TResult">the type of the cast</typeparam>
        /// <param name="source">the original list</param>
        /// <param name="noNullsInUncastedList">determines if the returned list can contain null values</param>
        /// <returns>a list of unsuccessfully casted objects</returns>
        public static IEnumerable<TSource> GetUncastable<TSource, TResult>(this IEnumerable<TSource> source
            , Boolean noNullsInUncastedList = false)
        {
            IEnumerable<TSource> filtered = source.Where(item => ((item is TResult) == false));

            if (noNullsInUncastedList)
            {
                filtered = filtered.Where(item => (item != null));
            }

            return (filtered);
        }
    }
}