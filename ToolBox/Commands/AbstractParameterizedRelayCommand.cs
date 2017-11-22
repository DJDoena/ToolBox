namespace DoenaSoft.ToolBox.Commands
{
    using System;
    using System.Windows.Input;

    public abstract class AbstractParameterizedRelayCommand : ICommand
    {
        private Func<Object, Boolean> CanExecuteCallback { get; }

        protected AbstractParameterizedRelayCommand(Func<Object, Boolean> canExecuteCallback = null)
        {
            CanExecuteCallback = canExecuteCallback;
        }

        #region ICommand 

        public Boolean CanExecute(Object parameter)
            => ((CanExecuteCallback != null) ? CanExecuteCallback(parameter) : true);

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