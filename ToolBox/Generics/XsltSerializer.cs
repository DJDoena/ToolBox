using System.Text;
using System.Xml;
using XmlS = System.Xml.Serialization;

namespace DoenaSoft.ToolBox.Generics;

/// <summary>
/// Generic class that serializes an XML with an XSLT prefix and suffix.
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class XsltSerializer<T>
    where T : class
{
    private static XmlS.XmlSerializer _serializer;

    private readonly IXsltSerializerDataProvider _dataProvider;

    /// <summary>
    /// Returns the enocding used when none is given
    /// </summary>
    public static Encoding DefaultEncoding { get; }

    /// <summary>
    /// The XmlSerializer primed for the data structure <typeparamref name="T"/>.
    /// </summary>
    public static XmlS.XmlSerializer Serializer
        => _serializer ??= new(typeof(T));

    static XsltSerializer()
    {
        DefaultEncoding = Encoding.UTF8;
    }

    /// <summary>
    /// Consturctor
    /// </summary>
    /// <param name="dataProvider">the data profiler for the XSLT prefix and suffix</param>
    public XsltSerializer(IXsltSerializerDataProvider dataProvider)
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

        Serializer.Serialize(writer, instance, new XmlS.XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

        var xml = encoding.GetString(ms.ToArray());

        return xml;
    }
}