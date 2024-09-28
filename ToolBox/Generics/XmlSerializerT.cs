using System.Text;
using System.Xml;
using XmlS = System.Xml.Serialization;

namespace DoenaSoft.ToolBox.Generics;

/// <summary>
/// Generic Serializer which contains methods to (de)serialize data structures to and from XML.
/// </summary>
/// <typeparam name="T">Type of the data structure</typeparam>
public static class XmlSerializer<T>
    where T : class, new()
{
    private static XmlS.XmlSerializer _serializer;

    /// <summary>
    /// Returns the enocding used when none is given
    /// </summary>
    public static Encoding DefaultEncoding { get; }

    static XmlSerializer()
    {
        DefaultEncoding = Encoding.UTF8;
    }

    /// <summary>
    /// The XmlSerializer primed for the data structure <typeparamref name="T"/>.
    /// </summary>
    public static XmlS.XmlSerializer Serializer
        => _serializer ??= new(typeof(T));

    /// <summary>
    /// Deserializes the content of <paramref name="fileName"/> into the data structure <typeparamref name="T"/>.
    /// </summary>
    /// <param name="fileName">The file name</param>
    /// <returns>An instance of <typeparamref name="T"/></returns>
    public static T Deserialize(string fileName)
    {
        using FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

        var result = Deserialize(fs);

        return result;
    }

    /// <summary>
    /// Deserializes the content of <paramref name="textReader"/> the data structure <typeparamref name="T"/>.
    /// </summary>
    /// <param name="textReader">The TextReader</param>
    /// <returns>An instance of <typeparamref name="T"/></returns>
    public static T Deserialize(TextReader textReader)
        => (T)Serializer.Deserialize(textReader);

    /// <summary>
    /// Deserializes the content of <paramref name="stream"/> the data structure <typeparamref name="T"/>.
    /// </summary>
    /// <param name="stream">The stream</param>
    /// <returns>An instance of <typeparamref name="T"/></returns>
    public static T Deserialize(Stream stream)
        => (T)Serializer.Deserialize(stream);

    /// <summary>
    /// Serializes an instance of the data structure <typeparamref name="T"/> into a file.
    /// </summary>
    /// <param name="fileName">The file name</param>
    /// <param name="instance">The data structure</param>
    /// <param name="encoding">The text encoding. Optional; if null then <see cref="Encoding.UTF8" /> is used.</param>
    public static void Serialize(string fileName
        , T instance
        , Encoding encoding = null)
    {
        using var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read);

        Serialize(fs, instance, encoding);
    }

    /// <summary>
    /// Serializes an instance of the data structure <typeparamref name="T"/> into a stream.
    /// </summary>
    /// <param name="stream">The stream</param>
    /// <param name="instance">The data structure</param>
    /// <param name="encoding">The text encoding. Optional; if null then <see cref="Encoding.UTF8" /> is used.</param>
    public static void Serialize(Stream stream
        , T instance
        , Encoding encoding = null)
    {
        encoding ??= DefaultEncoding;

        using var xw = XmlWriter.Create(stream, new()
        {
            CheckCharacters = true,
            Encoding = encoding,
            Indent = true,
            NamespaceHandling = NamespaceHandling.OmitDuplicates,
        });

        Serialize(xw, instance);
    }

    /// <summary>
    /// Serializes an instance of the data structure <typeparamref name="T"/> into an XmlWriter.
    /// </summary>
    /// <param name="xmlWriter">The XmlWriter</param>
    /// <param name="instance">The data structure</param>
    public static void Serialize(XmlWriter xmlWriter
        , T instance)
    {
        var ns = new XmlS.XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

        Serializer.Serialize(xmlWriter, instance, ns);
    }

    /// <summary>
    /// Deserializes the content of <paramref name="text"/> the data structure <typeparamref name="T"/>.
    /// </summary>
    /// <param name="text">The text</param>
    /// <param name="encoding">The text encoding. Optional; if null then <see cref="Encoding.UTF8" /> is used.</param>
    /// <returns>An instance of <typeparamref name="T"/></returns>
    public static T FromString(string text
        , Encoding encoding = null)
    {
        encoding ??= DefaultEncoding;

        using var ms = new MemoryStream(encoding.GetBytes(text));

        var result = Deserialize(ms);

        return result;
    }

    /// <summary>
    /// Serializes an instance of the data structure <typeparamref name="T"/> into a string.
    /// </summary>
    /// <param name="instance">The data structure</param>
    /// <param name="encoding">The text encoding. Optional; if null then <see cref="Encoding.UTF8" /> is used.</param>
    /// <returns></returns>
    public static string ToString(T instance
        , Encoding encoding = null)
    {
        encoding ??= DefaultEncoding;

        using var ms = new MemoryStream();

        Serialize(ms, instance, encoding);

        var result = encoding.GetString(ms.ToArray());

        return result;
    }
}