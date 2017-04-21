using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DoenaSoft.ToolBox.Commands
{
    public sealed class RelayCommandAsync : RelayCommand
    {
        public RelayCommandAsync(Action executeCallback
            , Func<Boolean> canExecuteCallback = null)
            : base(executeCallback, canExecuteCallback)
        { }

        #region ICommand

        public override void Execute(Object parameter)
        {
            if (CanExecute(parameter))
            {
                Task task = Task.Run(ExecuteCallback);

                task.ContinueWith(t => CommandManager.InvalidateRequerySuggested(), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }

        #endregion
    }
}