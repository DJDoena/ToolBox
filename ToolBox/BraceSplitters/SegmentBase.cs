namespace DoenaSoft.ToolBox.BraceSplitters;

/// <summary>
/// Base class to all segments.
/// </summary>
public abstract class SegmentBase
{
    /// <summary>
    /// Returns the plain text of the segment.
    /// </summary>
    public abstract string Text { get; }

    /// <summary>
    /// Converts the segment to its plain text representation.
    /// </summary>
    /// <returns></returns>
    public override sealed string ToString()
        => this.Text;
}