namespace DoenaSoft.ToolBox.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public sealed class CancelableCommandAsync : AbstractRelayCommand, ICancelableCommand
    {
        private Action<CancellationToken> ExecuteCallback { get; }

        public CancellationTokenSource CancellationTokenSource { get; set; }

        public CancelableCommandAsync(Action<CancellationToken> executeCallback
            , Func<Boolean> canExecuteCallback = null)
            : base(canExecuteCallback)
        {
            ExecuteCallback = executeCallback ?? throw (new ArgumentNullException(nameof(executeCallback)));
        }

        #region ICommand

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