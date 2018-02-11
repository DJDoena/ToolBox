namespace DoenaSoft.ToolBox.Generics
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Generic Serializer which contains methods to (de)serialize data structures to and from XML.
    /// </summary>
    /// <typeparam name="T">Type of the data structure</typeparam>
    public static class Serializer<T> where T : class, new()
    {
        private static XmlSerializer s_XmlSerializer;

        private static Encoding DefaultEncoding { get; }

        static Serializer()
        {
            DefaultEncoding = Encoding.UTF8;
        }

        /// <summary>
        /// The XmlSerializer primed for the data structure <typeparamref name="T"/>.
        /// </summary>
        public static XmlSerializer XmlSerializer
        {
            get
            {
                if (s_XmlSerializer == null)
                {
                    s_XmlSerializer = new XmlSerializer(typeof(T));
                }

                return (s_XmlSerializer);
            }
        }

        /// <summary>
        /// Deserializes the content of <paramref name="fileName"/> into the data structure <typeparamref name="T"/>.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns>An instance of <typeparamref name="T"/></returns>
        public static T Deserialize(String fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return (Deserialize(fs));
            }
        }

        /// <summary>
        /// Deserializes the content of <paramref name="textReader"/> the data structure <typeparamref name="T"/>.
        /// </summary>
        /// <param name="textReader">The TextReader</param>
        /// <returns>An instance of <typeparamref name="T"/></returns>
        public static T Deserialize(TextReader textReader)
            => (T)(XmlSerializer.Deserialize(textReader));

        /// <summary>
        /// Deserializes the content of <paramref name="stream"/> the data structure <typeparamref name="T"/>.
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <returns>An instance of <typeparamref name="T"/></returns>
        public static T Deserialize(Stream stream)
            => (T)(XmlSerializer.Deserialize(stream));

        /// <summary>
        /// Serializes an instance of the data structure <typeparamref name="T"/> into a file.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="instance">The data structure</param>
        /// <param name="encoding">The text encoding. Optional; if null then <see cref="Encoding.UTF8" /> is used.</param>
        public static void Serialize(String fileName
            , T instance
            , Encoding encoding = null)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                Serialize(fs, instance, encoding);
            }
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
            encoding = encoding ?? DefaultEncoding;

            using (XmlTextWriter xtw = new XmlTextWriter(stream, encoding))
            {
                xtw.Formatting = Formatting.Indented;
                xtw.Namespaces = false;

                Serialize(xtw, instance);
            }
        }

        /// <summary>
        /// Serializes an instance of the data structure <typeparamref name="T"/> into an XmlWriter.
        /// </summary>
        /// <param name="xmlWriter">The XmlWriter</param>
        /// <param name="instance">The data structure</param>
        public static void Serialize(XmlWriter xmlWriter
            , T instance)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            ns.Add(String.Empty, String.Empty);

            XmlSerializer.Serialize(xmlWriter, instance, ns);
        }

        /// <summary>
        /// Deserializes the content of <paramref name="text"/> the data structure <typeparamref name="T"/>.
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="encoding">The text encoding. Optional; if null then <see cref="Encoding.UTF8" /> is used.</param>
        /// <returns>An instance of <typeparamref name="T"/></returns>
        public static T FromString(String text
            , Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;

            using (Stream ms = new MemoryStream(encoding.GetBytes(text)))
            {
                return (Deserialize(ms));
            }
        }

        /// <summary>
        /// Serializes an instance of the data structure <typeparamref name="T"/> into a string.
        /// </summary>
        /// <param name="instance">The data structure</param>
        /// <param name="encoding">The text encoding. Optional; if null then <see cref="Encoding.UTF8" /> is used.</param>
        /// <returns></returns>
        public static String ToString(T instance
            , Encoding encoding = null)
        {
            encoding = encoding ?? DefaultEncoding;

            using (MemoryStream ms = new MemoryStream())
            {
                Serialize(ms, instance, encoding);

                return (encoding.GetString(ms.ToArray()));
            }
        }
    }
}