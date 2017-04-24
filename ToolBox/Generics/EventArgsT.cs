using System;

namespace DoenaSoft.ToolBox.Generics
{
    public sealed class EventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public EventArgs(T value)
        {
            Value = value;
        }
    }
}