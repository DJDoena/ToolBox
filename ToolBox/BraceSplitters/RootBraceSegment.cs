namespace DoenaSoft.ToolBox.BraceSplitters;

internal sealed class RootBraceSegment : BraceSegment
{
    public const char NullTerminator = '\0';

    public RootBraceSegment()
        : base(NullTerminator, NullTerminator)
    {
    }
}