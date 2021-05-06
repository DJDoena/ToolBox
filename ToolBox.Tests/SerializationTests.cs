using System.Collections.Generic;
using System.Text;
using DoenaSoft.ToolBox.Generics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DoenaSoft.ToolBox.Tests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void SerializeToUtf8String()
        {
            var dataObject = new DataObject()
            {
                Array = new SubDataObject[0],
                List = new List<SubDataObject>()
                {
                    new SubDataObject()
                    {
                        Prop1 = 42,
                        Prop2 = "Question",
                    },
                },
            };

            var xml = Serializer<DataObject>.ToString(dataObject, Encoding.UTF8);

            Assert.AreEqual("﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DataObject>\r\n  <Array />\r\n  <List>\r\n    <ListItem Prop1=\"42\" Prop2=\"Question\" />\r\n  </List>\r\n</DataObject>", xml);
        }

        [TestMethod]
        public void SerializeToIso8859String()
        {
            var dataObject = new DataObject()
            {
                Array = new SubDataObject[0],
                List = new List<SubDataObject>()
                {
                    new SubDataObject()
                    {
                        Prop1 = 42,
                        Prop2 = "Question",
                    },
                },
            };

            var xml = Serializer<DataObject>.ToString(dataObject, Encoding.GetEncoding("ISO-8859-1"));

            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>\r\n<DataObject>\r\n  <Array />\r\n  <List>\r\n    <ListItem Prop1=\"42\" Prop2=\"Question\" />\r\n  </List>\r\n</DataObject>", xml);
        }

        [TestMethod]
        public void InvalidCharsToUtf8String()
        {
            var dataObject = new DataObject()
            {
                Array = new[]
                {
                    new SubDataObject()
                    {
                        Prop2 = "„“«»‹›‚‘\"'",
                    },
                },
                Prop = "„“«»‹›‚‘\"'",
            };

            var xml = Serializer<DataObject>.ToString(dataObject, Encoding.UTF8);

            Assert.AreEqual("﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<DataObject>\r\n  <Array>\r\n    <ArrayItem Prop1=\"0\" Prop2=\"„“«»‹›‚‘&quot;'\" />\r\n  </Array>\r\n  <Prop>„“«»‹›‚‘\"'</Prop>\r\n</DataObject>", xml);
        }

        [TestMethod]
        public void InvalidCharsToIso8859String()
        {
            var dataObject = new DataObject()
            {
                Array = new[]
                {
                    new SubDataObject()
                    {
                        Prop2 = "„“«»‹›‚‘\"'",
                    },
                },
                Prop = "„“«»‹›‚‘\"'",
            };

            var xml = Serializer<DataObject>.ToString(dataObject, Encoding.GetEncoding("ISO-8859-1"));

            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>\r\n<DataObject>\r\n  <Array>\r\n    <ArrayItem Prop1=\"0\" Prop2=\"&#x201E;&#x201C;«»&#x2039;&#x203A;&#x201A;&#x2018;&quot;'\" />\r\n  </Array>\r\n  <Prop>&#x201E;&#x201C;«»&#x2039;&#x203A;&#x201A;&#x2018;\"'</Prop>\r\n</DataObject>", xml);
        }

    }
}
