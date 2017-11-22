namespace DoenaSoft.ToolBox.Commands
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

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