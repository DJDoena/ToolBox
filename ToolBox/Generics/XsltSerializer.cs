using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DoenaSoft.ToolBox.Generics;

/// <summary>
/// Generic class that serializes an XML with an XSLT prefix and suffix.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class XsltSerializer<T>
    where T : class, new()
{
    private readonly IXsltSerializerDataProvider<T> _dataProvider;

    /// <summary>
    /// Returns the enocding used when none is given
    /// </summary>
    public static Encoding DefaultEncoding
        => XmlSerializer<T>.DefaultEncoding;

    /// <summary>
    /// Consturctor
    /// </summary>
    /// <param name="dataProvider">the data profiler for the XSLT prefix and suffix</param>
    public XsltSerializer(IXsltSerializerDataProvider<T> dataProvider)
    {
        _dataProvider = dataProvider;
    }

    /// <summary>
    /// Serializes an instance of the data structure <typeparamref name="T"/> into a file with an XSLT prefix and suffix
    /// </summary>
    /// <param name="fileName">The file name</param>
    /// <param name="instance">The data structure</param>
    /// <param name="encoding">The text encoding. Optional; if null then <see cref="Encoding.UTF8" /> is used.</param>
    public void Serialize(string fileName
        , T instance
        , Encoding encoding = null)
    {
        var outputFile = new FileInfo(fileName);

        var xml = GetXmlString(instance);

        xml = "\t" + xml.TrimStart().Replace(Environment.NewLine, Environment.NewLine + "\t");

        using var sw = new StreamWriter(outputFile.FullName, false, encoding ?? DefaultEncoding);

        sw.WriteLine(_dataProvider.GetPrefix());
        sw.WriteLine(xml);
        sw.Write(_dataProvider.GetSuffix());
    }

    private static string GetXmlString(T instance)
    {
        using var ms = new MemoryStream();

        var encoding = Encoding.UTF8;

        var settings = new XmlWriterSettings()
        {
            Encoding = encoding,
            Indent = true,
            OmitXmlDeclaration = true,
            IndentChars = "\t",
        };

        using var writer = XmlWriter.Create(ms, settings);

        XmlSerializer<T>.Serializer.Serialize(writer, instance, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

        var xml = encoding.GetString(ms.ToArray());

        return xml;
    }
}