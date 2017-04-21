using System;

namespace DoenaSoft.ToolBox.Commands
{
    public class RelayCommand : AbstractRelayCommand
    {
        protected readonly Action ExecuteCallback;

        public RelayCommand(Action executeCallback
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
            if (CanExecute(parameter))
            {
                ExecuteCallback();
            }
        }

        #endregion
    }
}