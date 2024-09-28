namespace DoenaSoft.ToolBox.Extensions;

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
}