using System.Collections.Generic;
using System.Xml.Serialization;

namespace DoenaSoft.ToolBox.Tests
{
    public class DataObject
    {
        [XmlArray("Array")]
        [XmlArrayItem("ArrayItem")]
        public SubDataObject[] Array;

        [XmlArray("List")]
        [XmlArrayItem("ListItem")]
        public List<SubDataObject> List;

        [XmlElement]
        public string Prop { get; set; }
    }

    public class SubDataObject
    {
        [XmlAttribute]
        public int Prop1 { get; set; }

        [XmlAttribute]
        public string Prop2 { get; set; }
    }
}
