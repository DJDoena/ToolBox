namespace DoenaSoft.ToolBox.BraceSplitters;

/// <summary>
/// Represents a plain text segment without braces.
/// </summary>
public sealed class TextSegment : SegmentBase
{
    /// <summary>
    /// Returns the plain text of the segment.
    /// </summary>
    public override string Text { get; }

    internal TextSegment(string text)
    {
        this.Text = text;
    }
}