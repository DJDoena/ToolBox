namespace DoenaSoft.ToolBox.Commands
{
    using System;
    using System.Windows.Input;

    public abstract class AbstractRelayCommand : ICommand
    {
        private Func<Boolean> CanExecuteCallback { get; }

        protected AbstractRelayCommand(Func<Boolean> canExecuteCallback = null)
        {
            CanExecuteCallback = canExecuteCallback;
        }

        #region ICommand

        public Boolean CanExecute(Object parameter)
            => ((CanExecuteCallback != null) ? CanExecuteCallback() : true);

        public abstract void Execute(Object parameter);

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (CanExecuteCallback != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (CanExecuteCallback != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        #endregion
    }
}