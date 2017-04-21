using System.Threading;
using System.Windows.Input;

namespace DoenaSoft.ToolBox.Commands
{
    public interface ICancelableCommand : ICommand
    {
        CancellationTokenSource CancellationTokenSource { get; }
    }
}