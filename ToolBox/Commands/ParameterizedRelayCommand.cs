namespace DoenaSoft.ToolBox.Commands
{
    using System;

    public sealed class ParameterizedRelayCommand : AbstractParameterizedRelayCommand
    {
        private Action<Object> ExecuteCallback { get; }

        public ParameterizedRelayCommand(Action<Object> executeCallback
            , Func<Object, Boolean> canExecuteCallback = null)
            : base(canExecuteCallback)
        {
            ExecuteCallback = executeCallback ?? throw (new ArgumentNullException(nameof(executeCallback)));
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