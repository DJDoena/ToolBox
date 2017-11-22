namespace DoenaSoft.ToolBox.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary />
    public static class SplitExtensions
    {
        /// <summary>
        /// Splits a collection into two lists based on the given true/false evaluation.
        /// <remarks>
        /// This method resolves the query at once for performance considerations.
        /// It does not use deferred execution!
        /// </remarks>
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <param name="source">the original list</param>
        /// <param name="predicate">the function delegate to the evaluator</param>
        /// <param name="trueList">the true results</param>
        /// <param name="falseList">the false results</param>
        public static void SplitOptimized<TSource>(this IEnumerable<TSource> source
            , Func<TSource, Boolean> predicate
            , out List<TSource> trueList
            , out List<TSource> falseList)
        {
            //The .ToList() is deliberately here
            //otherwise the predicate would always be checked twice.
            //First when the user of the method iterates over the trueList
            //Second when the user of the method iterates over the falseList

            trueList = source.Where(predicate).ToList();

            falseList = source.Except(trueList).ToList();
        }

        /// <summary>
        /// Splits a collection into two lists based on the given true/false evaluation
        /// </summary>
        /// <typeparam name="TSource">the type of the list items</typeparam>
        /// <param name="source">the original list</param>
        /// <param name="predicate">the function delegate to the evaluator</param>
        /// <param name="trueList">the true results</param>
        /// <param name="falseList">the false results</param>
        public static void Split<TSource>(this IEnumerable<TSource> source
            , Func<TSource, Boolean> predicate
            , out IEnumerable<TSource> trueList
            , out IEnumerable<TSource> falseList)
        {
            trueList = source.Where(predicate);

            falseList = source.Except(trueList);
        }
    }
}