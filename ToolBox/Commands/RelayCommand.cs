namespace DoenaSoft.ToolBox.Commands
{
    using System;

    /// <summary>
    /// Basic command.
    /// </summary>
    public class RelayCommand : AbstractRelayCommand
    {
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        protected Action ExecuteCallback { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="executeCallback">Defines the method to be called when the command is invoked</param>
        /// <param name="canExecuteCallback">Defines the method that determines whether the command can execute in its current state</param>
        public RelayCommand(Action executeCallback
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
            if (CanExecute(parameter))
            {
                ExecuteCallback();
            }
        }

        #endregion
    }
}