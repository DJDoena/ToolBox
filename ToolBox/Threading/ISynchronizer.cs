namespace DoenaSoft.ToolBox.Threading
{
    using System;

    /// <summary>
    /// Interface to invoke methods from a secondary thread back onto the UI thread.
    /// </summary>
    public interface ISynchronizer
    {
        /// <summary>
        /// Invokes an action on the UI thread.
        /// </summary>
        /// <param name="action">The action</param>
        void InvokeOnUIThread(Action action);

        /// <summary>
        /// Invokes a function on the UI thread.
        /// </summary>
        /// <param name="func">The function</param>
        T InvokeOnUIThread<T>(Func<T> func);
    }
}