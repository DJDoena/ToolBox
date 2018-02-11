namespace DoenaSoft.ToolBox.Commands
{
    using System.Threading;
    using System.Windows.Input;

    /// <summary>
    /// Interface for commands that can be cancelled.
    /// </summary>
    public interface ICancelableCommand : ICommand
    {
        /// <summary>
        /// Signals to a <see cref="CancellationToken"/> that it should be canceled.
        /// </summary>
        CancellationTokenSource CancellationTokenSource { get; }
    }
}