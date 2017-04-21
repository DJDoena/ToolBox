using System;

namespace DoenaSoft.ToolBox.Threading
{
    public interface ISynchronizer
    {
        void InvokeOnUIThread(Action action);

        T InvokeOnUIThread<T>(Func<T> func);
    }
}