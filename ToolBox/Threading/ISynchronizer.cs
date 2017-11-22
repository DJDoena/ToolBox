namespace DoenaSoft.ToolBox.Threading
{
    using System;

    public interface ISynchronizer
    {
        void InvokeOnUIThread(Action action);

        T InvokeOnUIThread<T>(Func<T> func);
    }
}