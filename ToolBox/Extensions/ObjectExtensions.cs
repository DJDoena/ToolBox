using System.Collections.Generic;

namespace DoenaSoft.ToolBox.Extensions
{
    /// <summary />
    public static class ObjectExtensions
    {
        /// <summary>
        /// Wraps an object into an <see cref="IEnumerable{TSource}"/> consisting of a single item (or an empty enumerable if the object is null).
        /// </summary>
        /// <typeparam name="TSource">type of the object.</typeparam>
        /// <param name="o">the object</param>
        /// <returns>the<see cref="IEnumerable{TSource}"/></returns>
        public static IEnumerable<TSource> Enumerate<TSource>(this TSource o)
        {
            if (o == null)
            {
                yield break;
            }

            yield return o;
        }

        /// <summary>
        /// Wraps a parameter list into an <see cref="IEnumerable{TSource}"/>.
        /// </summary>
        /// <typeparam name="TSource">type of the objects</typeparam>
        /// <param name="objects">the parameter list</param>
        /// <returns>the <see cref="IEnumerable{TSource}"/></returns>
        public static IEnumerable<TSource> Enumerate<TSource>(params TSource[] objects)
        {
            if (objects == null)
            {
                yield break;
            }

            foreach (TSource o in objects)
            {
                yield return o;
            }
        }
    }
}