using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ESB.Extensions.Resolution;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ESB.Extensions.Resolution.Tests
{
    [TestClass]
    public class ResolutionDictionaryTests
    {
        public ResolutionDictionaryTests()
        {
        }

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void T0001_SerializationRoundtrip()
        {
            TestClass tc = new TestClass();
            tc.Value = "SomeTestClassValue";
            TestStruct ts = new TestStruct();
            ts.Value = "SomeTestStructValue";

            ResolutionDictionary rd = new ResolutionDictionary();
            rd.SetValue("SomeKey", "SomeValue");
            rd.SetValue("SomeTestClassKey", tc);
            rd.SetValue("SomeTestStructKey", ts);

            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, rd);

            Assert.IsTrue(ms.Length > 0);
            ms.Seek(0, SeekOrigin.Begin);

            ResolutionDictionary rd2 = bf.Deserialize(ms) as ResolutionDictionary;
            Assert.AreEqual<int>(rd.Count, rd2.Count);

            string v2 = rd.GetString("SomeKey");
            Assert.AreEqual<string>("SomeValue", v2);

            TestClass tc2 = rd2.GetValue("SomeTestClassKey") as TestClass;
            Assert.AreEqual<string>(tc.Value, tc2.Value);

            TestStruct ts2 = (TestStruct) rd2.GetValue("SomeTestStructKey");
            Assert.AreEqual<string>(ts.Value, ts2.Value);
        }
    }

    [Serializable]
    public class TestClass
    {
        public string Value;
    }

    [Serializable]
    public struct TestStruct
    {
        public string Value;
    }
}
