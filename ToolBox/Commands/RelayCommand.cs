namespace DoenaSoft.ToolBox.Commands
{
    using System;

    public class RelayCommand : AbstractRelayCommand
    {
        protected Action ExecuteCallback { get; }

        public RelayCommand(Action executeCallback
            , Func<Boolean> canExecuteCallback = null)
            : base(canExecuteCallback)
        {
            ExecuteCallback = executeCallback ?? throw (new ArgumentNullException(nameof(executeCallback)));
        }

        #region ICommand

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