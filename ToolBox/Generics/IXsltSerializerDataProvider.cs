namespace DoenaSoft.ToolBox.Generics;

/// <summary>
/// Interface to provide the XSLT prefix and suffix for the XsltSerializer
/// </summary>
public interface IXsltSerializerDataProvider
{
    /// <summary>
    /// Returns the XSLT prefix.
    /// </summary>
    string GetPrefix();

    /// <summary>
    /// Returns the XSLT suffix.
    /// </summary>
    string GetSuffix();
}