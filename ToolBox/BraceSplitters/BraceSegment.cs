namespace DoenaSoft.ToolBox.BraceSplitters;

/// <summary>
/// Represents a segment surrouned by a set of braces.
/// </summary>
public class BraceSegment : SegmentBase
{
    /// <summary>
    /// Returns the plain text of the segment.
    /// </summary>
    public override string Text
    {
        get
        {
            var result = string.Join(string.Empty, this.Segments);

            if (this.OpeningBrace != RootBraceSegment.NullTerminator)
            {
                result = $"{this.OpeningBrace}{result}{this.ClosingBrace}";
            }

            return result;
        }
    }

    /// <summary>
    /// Returns all sub-segments contained within the braces.
    /// </summary>
    public List<SegmentBase> Segments { get; }

    /// <summary>
    /// Returns the character used as the opening brace.
    /// </summary>
    public char OpeningBrace { get; }

    /// <summary>
    /// Returns the character used as the closing brace.
    /// </summary>
    public char ClosingBrace { get; }

    internal BraceSegment(char openingBrace
        , char closingBrace)
    {
        this.OpeningBrace = openingBrace;

        this.ClosingBrace = closingBrace;

        this.Segments = [];
    }
}