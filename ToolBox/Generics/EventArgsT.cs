namespace DoenaSoft.ToolBox.Generics
{
    public sealed class EventArgs<T>
    {
        public T Value { get; private set; }

        public EventArgs(T value)
        {
            Value = value;
        }
    }
}