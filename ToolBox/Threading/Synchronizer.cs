namespace DoenaSoft.ToolBox.Threading
{
    using System;
    using System.Windows.Threading;

    /// <summary>
    /// Class to invoke methods from a secondary thread back onto the UI thread.
    /// </summary>
    public sealed class Synchronizer : ISynchronizer
    {
        private Dispatcher Dispatcher { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dispatcher">The dispatcher to invoke methods on the UI thread</param>
        public Synchronizer(Dispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        #region ISynchronizer

        /// <summary>
        /// Invokes a function on the UI thread.
        /// </summary>
        /// <param name="func">The function</param>
        public T InvokeOnUIThread<T>(Func<T> func)
          => Dispatcher.CheckAccess() ? func() : Dispatcher.Invoke(func);

        /// <summary>
        /// Invokes an action on the UI thread.
        /// </summary>
        /// <param name="action">The action</param>
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