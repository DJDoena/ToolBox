using DoenaSoft.ToolBox.BraceSplitters;

namespace DoenaSoft.ToolBox.Tests;

[TestClass]
public sealed class BraceSplitterTests
{
    [TestMethod]
    public void NoBraces()
    {
        var result = (new BraceSplitter()).Split("abcdefghij");

        Assert.AreEqual("abcdefghij", result.ToString());

        Assert.IsInstanceOfType<TextSegment>(result);

        Assert.AreEqual("abcdefghij", result.ToString());
    }

    [TestMethod]
    public void SimpleOuterBraces()
    {
        var result = (new BraceSplitter()).Split("(abcdefghij)");

        Assert.AreEqual("(abcdefghij)", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(1, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("abcdefghij", segment.Segments[0].ToString());
    }

    [TestMethod]
    public void SimpleInnerBraces()
    {
        var result = (new BraceSplitter()).Split("abcd(ef)ghij");

        Assert.AreEqual("abcd(ef)ghij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(3, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("abcd", segment.Segments[0].ToString());

        var segment1 = segment.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1);

        Assert.HasCount(1, segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[0]);

        Assert.AreEqual("ef", segment1.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[2]);

        Assert.AreEqual("ghij", segment.Segments[2].ToString());
    }

    [TestMethod]
    public void EmptyInnerBraces()
    {
        var result = (new BraceSplitter()).Split("abcd()ghij");

        Assert.AreEqual("abcd()ghij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(3, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("abcd", segment.Segments[0].ToString());

        var segment1 = segment.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1);

        Assert.HasCount(1, segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[0]);

        Assert.AreEqual("", segment1.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[2]);

        Assert.AreEqual("ghij", segment.Segments[2].ToString());
    }

    [TestMethod]
    public void EmptyOuterBraces()
    {
        var result = (new BraceSplitter()).Split("()");

        Assert.AreEqual("()", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(1, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("", segment.Segments[0].ToString());
    }

    [TestMethod]
    public void SequentialBracesWithoutGap()
    {
        var result = (new BraceSplitter()).Split("ab(cd)(ef)ghij");

        Assert.AreEqual("ab(cd)(ef)ghij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(4, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("ab", segment.Segments[0].ToString());

        var segment1 = segment.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1);

        Assert.HasCount(1, segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[0]);

        Assert.AreEqual("cd", segment1.Segments[0].ToString());

        var segment2 = segment.Segments[2] as BraceSegment;

        Assert.IsNotNull(segment2);

        Assert.HasCount(1, segment2.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment2.Segments[0]);

        Assert.AreEqual("ef", segment2.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[3]);

        Assert.AreEqual("ghij", segment.Segments[3].ToString());
    }

    [TestMethod]
    public void SequentialBracesWithGap()
    {
        var result = (new BraceSplitter()).Split("ab(cd)ef(gh)ij");

        Assert.AreEqual("ab(cd)ef(gh)ij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(5, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("ab", segment.Segments[0].ToString());

        var segment1 = segment.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1);

        Assert.HasCount(1, segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[0]);

        Assert.AreEqual("cd", segment1.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[2]);

        Assert.AreEqual("ef", segment.Segments[2].ToString());

        var segment3 = segment.Segments[3] as BraceSegment;

        Assert.IsNotNull(segment3);

        Assert.HasCount(1, segment3.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment3.Segments[0]);

        Assert.AreEqual("gh", segment3.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[4]);

        Assert.AreEqual("ij", segment.Segments[4].ToString());
    }

    /// <remarks>
    /// The results will not be embedded into each other since this is not a DOM parser.
    /// </remarks>
    [TestMethod]
    public void HtmlTest()
    {
        var result = (new BraceSplitter(searchForParenthesis: false, searchForChevrons: true)).Split("<html><head></head><body><h1>Hello World</h1></body></html>");

        Assert.AreEqual("<html><head></head><body><h1>Hello World</h1></body></html>", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(9, segment.Segments);
    }

    [TestMethod]
    public void EmbeddedBraces()
    {
        var result = (new BraceSplitter()).Split("ab(cd(ef)gh)ij");

        Assert.AreEqual("ab(cd(ef)gh)ij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(3, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("ab", segment.Segments[0].ToString());

        var segment1 = segment.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1);

        Assert.HasCount(3, segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[0]);

        Assert.AreEqual("cd", segment1.Segments[0].ToString());

        var segment1segment1 = segment1.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1segment1);

        Assert.HasCount(1, segment1segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1segment1.Segments[0]);

        Assert.AreEqual("ef", segment1segment1.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[2]);

        Assert.AreEqual("gh", segment1.Segments[2].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[2]);

        Assert.AreEqual("ij", segment.Segments[2].ToString());
    }

    [TestMethod]
    public void EmbeddedMixedBraces()
    {
        var result = (new BraceSplitter(searchForBrackets: true, searchForCurlyBraces: true)).Split("ab{cd[ef]gh}ij");

        Assert.AreEqual("ab{cd[ef]gh}ij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(3, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("ab", segment.Segments[0].ToString());

        var segment1 = segment.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1);

        Assert.HasCount(3, segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[0]);

        Assert.AreEqual("cd", segment1.Segments[0].ToString());

        var segment1segment1 = segment1.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1segment1);

        Assert.HasCount(1, segment1segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1segment1.Segments[0]);

        Assert.AreEqual("ef", segment1segment1.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[2]);

        Assert.AreEqual("gh", segment1.Segments[2].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[2]);

        Assert.AreEqual("ij", segment.Segments[2].ToString());
    }

    [TestMethod]
    public void EmbeddedMixedBracesOnlyOpenCurly()
    {
        var result = (new BraceSplitter(searchForBrackets: true, searchForCurlyBraces: true)).Split("ab{cd[efgh}ij");

        Assert.AreEqual("ab{cd[efgh}ij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(3, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("ab", segment.Segments[0].ToString());

        var segment1 = segment.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1);

        Assert.HasCount(2, segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[0]);

        Assert.AreEqual("cd", segment1.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[1]);

        Assert.AreEqual("[efgh", segment1.Segments[1].ToString());
    }

    [TestMethod]
    public void EmbeddedMixedBracesOnlyOpenBracket()
    {
        var result = (new BraceSplitter(searchForBrackets: true, searchForCurlyBraces: true)).Split("ab{cd[efgh}ij");

        Assert.AreEqual("ab{cd[efgh}ij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(3, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("ab", segment.Segments[0].ToString());

        var segment1 = segment.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1);

        Assert.HasCount(2, segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[0]);

        Assert.AreEqual("cd", segment1.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[1]);

        Assert.AreEqual("[efgh", segment1.Segments[1].ToString());
    }

    [TestMethod]
    public void MoreOpenThanClose()
    {
        var result = (new BraceSplitter()).Split("ab(cd(efgh)ij");

        Assert.AreEqual("ab(cd(efgh)ij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(2, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("ab", segment.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[1]);

        Assert.AreEqual("(cd(efgh)ij", segment.Segments[1].ToString());
    }

    [TestMethod]
    public void MoreCloseThanClose()
    {
        var result = (new BraceSplitter()).Split("ab(cd)efgh)ij");

        Assert.AreEqual("ab(cd)efgh)ij", result.ToString());

        var segment = result as BraceSegment;

        Assert.IsNotNull(segment);

        Assert.HasCount(3, segment.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[0]);

        Assert.AreEqual("ab", segment.Segments[0].ToString());

        var segment1 = segment.Segments[1] as BraceSegment;

        Assert.IsNotNull(segment1);

        Assert.HasCount(1, segment1.Segments);

        Assert.IsInstanceOfType<TextSegment>(segment1.Segments[0]);

        Assert.AreEqual("cd", segment1.Segments[0].ToString());

        Assert.IsInstanceOfType<TextSegment>(segment.Segments[2]);

        Assert.AreEqual("efgh)ij", segment.Segments[2].ToString());
    }
}
