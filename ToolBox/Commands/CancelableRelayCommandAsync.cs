namespace DoenaSoft.ToolBox.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    /// <summary>
    /// An asynchronous command which can be cancelled.
    /// </summary>
    public sealed class CancelableRelayCommandAsync : AbstractRelayCommand, ICancelableCommand
    {
        private Action<CancellationToken> ExecuteCallback { get; }

        /// <summary>
        /// Signals to a <see cref="CancellationToken"/> that it should be canceled.
        /// </summary>
        public CancellationTokenSource CancellationTokenSource { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="executeCallback">Defines the method to be called when the command is invoked</param>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        public CancelableRelayCommandAsync(Action<CancellationToken> executeCallback
            , Func<Boolean> canExecuteCallback = null)
            : base(canExecuteCallback)
        {
            ExecuteCallback = executeCallback ?? throw (new ArgumentNullException(nameof(executeCallback)));
        }

        #region ICommand

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        public override void Execute(Object parameter)
        {
            CancellationTokenSource = null;

            if (CanExecute(parameter))
            {
                CancellationTokenSource = new CancellationTokenSource();

                CancellationToken ct = CancellationTokenSource.Token;

                Task task = Task.Run(() => ExecuteCallback(ct), ct);

                task.ContinueWith(t => CommandManager.InvalidateRequerySuggested(), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        #endregion
    }
}