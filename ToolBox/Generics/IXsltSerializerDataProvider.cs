namespace DoenaSoft.ToolBox.Generics;

/// <summary>
/// Interface to provide the XSLT prefix and suffix for the XsltSerializer
/// </summary>
/// <typeparam name="T">Type of the data structure</typeparam>
public interface IXsltSerializerDataProvider<T>
    where T : class, new()
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