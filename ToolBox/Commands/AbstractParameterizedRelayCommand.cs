namespace DoenaSoft.ToolBox.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Base class for commands with parameters.
    /// </summary>
    public abstract class AbstractParameterizedRelayCommand : ICommand
    {
        private Func<Object, Boolean> CanExecuteCallback { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        protected AbstractParameterizedRelayCommand(Func<Object, Boolean> canExecuteCallback = null)
        {
            CanExecuteCallback = canExecuteCallback;
        }

        #region ICommand 

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null</param>
        /// <returns>true if this command can be executed; otherwise, false</returns>
        public Boolean CanExecute(Object parameter)
            => ((CanExecuteCallback != null) ? CanExecuteCallback(parameter) : true);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null</param>
        public abstract void Execute(Object parameter);

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
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