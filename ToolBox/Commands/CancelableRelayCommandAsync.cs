using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoenaSoft.ToolBox.Commands
{
    public sealed class CancelableCommandAsync : AbstractRelayCommand, ICancelableCommand
    {
        private readonly Action<CancellationToken> ExecuteCallback;

        public CancellationTokenSource CancellationTokenSource { get; set; }

        public CancelableCommandAsync(Action<CancellationToken> executeCallback
            , Func<Boolean> canExecuteCallback = null)
            : base(canExecuteCallback)
        {
            if (executeCallback == null)
            {
                throw (new ArgumentNullException(nameof(executeCallback)));
            }

            ExecuteCallback = executeCallback;
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