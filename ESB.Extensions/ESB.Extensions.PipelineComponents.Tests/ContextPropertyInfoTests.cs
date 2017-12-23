using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml.Serialization;
using System.IO;

using Microsoft.BizTalk.Component.Interop;

using ESB.Extensions.PipelineComponents.Tests.Properties;
using ESB.Extensions.PipelineConponents.ContextAdder;

namespace ESB.Extensions.PipelineComponents.Tests
{
    [TestClass]
    public class ContextPropertyInfoTests
    {
        public ContextPropertyInfoTests()
        {
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void T0001_SerializationRoundtripWithCDATA()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ContextPropertyInfoCollection));
            ContextPropertyInfo cpi = CreateContextPropertyInfoWithCDATA();

            ContextPropertyInfoCollection cpic = new ContextPropertyInfoCollection();
            cpic.Add(cpi);

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                xs.Serialize(sw, cpic);
            }
            string xml = sb.ToString();
            Assert.IsTrue(xml.Contains(@"<PropertyValue>&lt;![CDATA[&lt;SomePropertyValue&gt;SomeValue&lt;![CDATA[SomeData]]&lt;/SomePropertyValue&gt;]]&gt;</PropertyValue>"));

            using (StringReader sr = new StringReader(xml))
            {
                ContextPropertyInfoCollection cpic2 = (ContextPropertyInfoCollection)xs.Deserialize(sr);
                ContextPropertyInfo cpi2 = cpic2[0];
                AssertEqual(cpi, cpi2);
            }
        }

        [TestMethod]
        public void T0002_SerializationRoundtripWithoutCDATA()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ContextPropertyInfo));
            ContextPropertyInfo cpi = CreateContextPropertyInfoWithoutCDATA();

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                xs.Serialize(sw, cpi);
            }
            string xml = sb.ToString();
            Assert.IsTrue(xml.Contains("<PropertyValue>SomePropertyValue</PropertyValue>"));

            using (StringReader sr = new StringReader(xml))
            {
                ContextPropertyInfo cpi2 = (ContextPropertyInfo)xs.Deserialize(sr);
                AssertEqual(cpi, cpi2);
            }
        }

        private ContextPropertyInfo CreateContextPropertyInfoWithCDATA()
        {
            ContextPropertyInfo cpi = new ContextPropertyInfo();
            cpi.PropertyName = "SomePropertyName";
            cpi.PropertyNamespace = "http://SomePropertyNamespace/SomeValue";
            cpi.PropertyValue = @"<![CDATA[<SomePropertyValue>SomeValue<![CDATA[SomeData]]</SomePropertyValue>]]>";
            cpi.Promoted = true;
            return cpi;
        }

        private ContextPropertyInfo CreateContextPropertyInfoWithoutCDATA()
        {
            ContextPropertyInfo cpi = new ContextPropertyInfo();
            cpi.PropertyName = "SomePropertyName";
            cpi.PropertyNamespace = "http://SomePropertyNamespace/SomeValue";
            cpi.PropertyValue = "SomePropertyValue";
            cpi.Promoted = true;
            return cpi;
        }

        private void AssertEqual(ContextPropertyInfo cpi1, ContextPropertyInfo cpi2)
        {
            Assert.IsTrue(cpi1.PropertyName.Equals(cpi2.PropertyName));
            Assert.IsTrue(cpi1.PropertyNamespace.Equals(cpi2.PropertyNamespace));
            Assert.IsTrue(cpi1.PropertyValue.Equals(cpi2.PropertyValue));
            Assert.IsTrue(cpi1.Promoted == cpi2.Promoted);
        }
    }

    public class TestBag : IPropertyBag
    {
        private Dictionary<string, object> _bag = new Dictionary<string,object>();

        #region IPropertyBag Members

        public void Read(string propName, out object ptrVar, int errorLog)
        {
            ptrVar = _bag[propName];
        }

        public void Write(string propName, ref object ptrVar)
        {
            _bag.Add(propName, ptrVar);
        }

        #endregion
    }
}
