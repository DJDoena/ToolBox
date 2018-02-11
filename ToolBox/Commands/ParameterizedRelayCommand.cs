namespace DoenaSoft.ToolBox.Commands
{
    using System;

    /// <summary>
    /// A command which can have a command parameter.
    /// </summary>
    public sealed class ParameterizedRelayCommand : AbstractParameterizedRelayCommand
    {
        private Action<Object> ExecuteCallback { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="executeCallback">Defines the method to be called when the command is invoked</param>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        public ParameterizedRelayCommand(Action<Object> executeCallback
            , Func<Object, Boolean> canExecuteCallback = null)
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
            if (CanExecute(parameter))
            {
                ExecuteCallback(parameter);
            }
        }

        #endregion     
    }
}