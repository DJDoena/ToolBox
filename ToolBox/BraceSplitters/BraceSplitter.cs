using System.Diagnostics;

namespace DoenaSoft.ToolBox.BraceSplitters;

/// <summary>
/// Splits a string into segments based on braces (parentheses "()", brackets "[]", curly braces "{}" and chevrons "&lt;&gt;".).
/// </summary>
public sealed class BraceSplitter
{
    private readonly bool _searchForParenthesis;

    private readonly bool _searchForBrackets;

    private readonly bool _searchForCurlyBraces;

    private readonly bool _searchForChevrons;

    private string _text;

    private BraceSegment _braceSegment;

    private int _lastStartIndex;

    private int _textIndex;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="searchForParenthesis">search for pairs of parentheses "(" and ")"</param>
    /// <param name="searchForBrackets">search for pairs of brackets "[" and "]"</param>
    /// <param name="searchForCurlyBraces">search for pairs of curly braces "{" and "}"</param>
    /// <param name="searchForChevrons">search for pairs of chevrons "&lt;" and "&gt;"</param>
    [DebuggerStepThrough]
    public BraceSplitter(bool searchForParenthesis = true
        , bool searchForBrackets = false
        , bool searchForCurlyBraces = false
        , bool searchForChevrons = false)
    {
        _searchForParenthesis = searchForParenthesis;
        _searchForBrackets = searchForBrackets;
        _searchForCurlyBraces = searchForCurlyBraces;
        _searchForChevrons = searchForChevrons;
    }

    /// <summary>
    /// Splits a string into segments based on braces (parentheses "()", brackets "[]", curly braces "{}").
    /// </summary>
    /// <param name="text">the text to be split</param>
    public SegmentBase Split(string text)
    {
        if (text is null)
        {
            return null;
        }
        else if (string.IsNullOrWhiteSpace(text))
        {
            return new TextSegment(text);
        }
        else if (!this.ContainsOpenBrace(text))
        {
            return new TextSegment(text);
        }

        var segment = new RootBraceSegment();

        this.SplitRecursively(text, segment);

        var result = GetRootSegment(segment);

        return result;
    }

    private bool ContainsOpenBrace(string text)
        => text.Any(c => this.IsOpenBrace(c, out _, out _));

    private bool IsOpenBrace(char character
        , out char openBrace
        , out char closeBrace)
    {
        if (_searchForParenthesis && character == '(')
        {
            openBrace = '(';

            closeBrace = ')';

            return true;
        }
        else if (_searchForBrackets && character == '[')
        {
            openBrace = '[';

            closeBrace = ']';

            return true;
        }
        else if (_searchForCurlyBraces && character == '{')
        {
            openBrace = '{';

            closeBrace = '}';

            return true;
        }
        else if (_searchForChevrons && character == '<')
        {
            openBrace = '<';

            closeBrace = '>';

            return true;
        }
        else
        {
            openBrace = RootBraceSegment.NullTerminator;

            closeBrace = RootBraceSegment.NullTerminator;

            return false;
        }
    }

    private static SegmentBase GetRootSegment(BraceSegment braceSegment)
    {
        if (braceSegment.Segments.Count == 1)
        {
            return braceSegment.Segments[0];
        }
        else
        {
            return braceSegment;
        }
    }

    private void SplitRecursively(string text
        , BraceSegment braceSegment)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            braceSegment.Segments.Add(new TextSegment(text));

            return;
        }
        else if (!this.ContainsOpenBrace(text))
        {
            braceSegment.Segments.Add(new TextSegment(text));

            return;
        }

        _text = text;

        _braceSegment = braceSegment;

        _lastStartIndex = 0;

        for (_textIndex = _lastStartIndex; _textIndex < text.Length; _textIndex++)
        {
            this.SplitText();
        }
    }

    private void SplitText()
    {
        if (this.IsOpenBrace(_text[_textIndex], out var openBrace, out var closeBrace))
        {
            if (_textIndex - _lastStartIndex > 0)
            {
                _braceSegment.Segments.Add(new TextSegment(_text.Substring(_lastStartIndex, _textIndex - _lastStartIndex)));
            }

            var openBracesCount = 0;
            for (var braceIndex = _textIndex + 1; braceIndex < _text.Length; braceIndex++)
            {
                var flowResult = this.SplitBrace(ref openBracesCount, braceIndex, openBrace, closeBrace);

                if (flowResult.HasValue && flowResult.Value)
                {
                    continue;
                }
                else if (flowResult.HasValue && !flowResult.Value)
                {
                    break;
                }
            }
        }
        else if (_textIndex == _text.Length - 1)
        {
            _braceSegment.Segments.Add(new TextSegment(_text.Substring(_lastStartIndex)));
        }
    }

    private bool? SplitBrace(ref int openBracesCount
        , int braceIndex
        , char openBrace
        , char closeBrace)
    {
        if (_text[braceIndex] == openBrace)
        {
            openBracesCount++;

            return true;
        }
        else if (_text[braceIndex] == closeBrace)
        {
            if (openBracesCount > 0)
            {
                openBracesCount--;

                return true;
            }
            else
            {
                var subText = _text.Substring(_textIndex + 1, braceIndex - _textIndex - 1);

                var subBraceSegment = new BraceSegment(openBrace, closeBrace);

                (new BraceSplitter(_searchForParenthesis, _searchForBrackets, _searchForCurlyBraces)).SplitRecursively(subText, subBraceSegment);

                _braceSegment.Segments.Add(subBraceSegment);
            }

            _textIndex = braceIndex;

            _lastStartIndex = braceIndex + 1;

            return false;
        }
        else if (braceIndex == _text.Length - 1)
        {
            var segment = (new BraceSplitter(searchForParenthesis: _searchForParenthesis && openBrace != '('
                , searchForBrackets: _searchForBrackets && openBrace != '['
                , searchForCurlyBraces: _searchForCurlyBraces && openBrace != '{'
                , searchForChevrons: _searchForChevrons && openBrace != '<'))
                .Split(_text.Substring(_textIndex));

            _braceSegment.Segments.Add(segment);

            _textIndex = braceIndex;

            _lastStartIndex = braceIndex + 1;

            return false;
        }

        return null;
    }
}