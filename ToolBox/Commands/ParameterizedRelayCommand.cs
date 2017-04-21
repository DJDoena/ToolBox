using System;

namespace DoenaSoft.ToolBox.Commands
{
    public sealed class ParameterizedRelayCommand : AbstractParameterizedRelayCommand
    {
        private readonly Action<Object> ExecuteCallback;

        public ParameterizedRelayCommand(Action<Object> executeCallback
            , Func<Object, Boolean> canExecuteCallback = null)
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
                ExecuteCallback(parameter);
            }
        }

        #endregion     
    }
}