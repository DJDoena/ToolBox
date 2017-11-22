namespace DoenaSoft.ToolBox.Commands
{
    using System.Threading;
    using System.Windows.Input;

    public interface ICancelableCommand : ICommand
    {
        CancellationTokenSource CancellationTokenSource { get; }
    }
}