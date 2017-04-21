using System;
using System.Windows.Threading;

namespace DoenaSoft.ToolBox.Threading
{
    public sealed class Synchronizer : ISynchronizer
    {
        private readonly Dispatcher Dispatcher;

        public Synchronizer(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        #region ISynchronizer

        public T InvokeOnUIThread<T>(Func<T> func)
          => (Dispatcher.CheckAccess() ? func() : Dispatcher.Invoke(func));

        public void InvokeOnUIThread(Action action)
        {
            if (Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                Dispatcher.Invoke(action);
            }
        }

        #endregion
    }
}