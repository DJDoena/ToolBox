namespace DoenaSoft.ToolBox.Generics
{
    using System;

    public sealed class EventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public EventArgs(T value)
        {
            Value = value;
        }
    }
}