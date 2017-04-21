using System;
using System.Windows.Input;

namespace DoenaSoft.ToolBox.Commands
{
    public abstract class AbstractRelayCommand : ICommand
    {
        private readonly Func<Boolean> CanExecuteCallback;

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