namespace DoenaSoft.ToolBox.Generics;

/// <summary>
/// Generic EventArgs which can carry a value.
/// </summary>
/// <typeparam name="T">Type of the value</typeparam>
public sealed class EventArgs<T> : EventArgs
{
    /// <summary>
    /// The value to be send with the event.
    /// </summary>
    public T Value { get; private set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="value">The value to be send with the event</param>
    public EventArgs(T value)
    {
        this.Value = value;
    }
}