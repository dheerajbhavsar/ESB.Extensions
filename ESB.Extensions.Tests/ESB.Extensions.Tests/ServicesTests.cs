using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESB.Extensions.Tests
{
    [TestClass]
    public class ServicesTests
    {
        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void T00050_PipelineTestOrderBatchDebatcherGoPublisher()
        {
            BizUnit.BizUnit but = new BizUnit.BizUnit(@"..\..\..\ESB.Extensions.Tests\TestCases\T00050-PipelineTest.OrderBatchDebatcher.GoPublisher.bizunit.xml");
            but.RunTest();
        }

        [TestMethod]
        public void T00100_OneWayDebatchMessageSendPort()
        {
            BizUnit.BizUnit but = new BizUnit.BizUnit(@"..\..\..\ESB.Extensions.Tests\TestCases\T00100-OneWay-Debatch-MessageSendPort.bizunit.xml");
            but.RunTest();
        }

        [TestMethod]
        public void T00200_OneWayDebatchAggregateMessageSendPort()
        {
            BizUnit.BizUnit but = new BizUnit.BizUnit(@"..\..\..\ESB.Extensions.Tests\TestCases\T00200-OneWay-Debatch-Aggregate-MessageSendPort.bizunit.xml");
            but.RunTest();
        }

        [TestMethod]
        public void T00210_OneWayDebatchAggregateMessageSendPort5Batches()
        {
            BizUnit.BizUnit but = new BizUnit.BizUnit(@"..\..\..\ESB.Extensions.Tests\TestCases\T00210-OneWay-Debatch-Aggregate-MessageSendPort-5-Batches.bizunit.xml");
            but.RunTest();
        }

        [TestMethod]
        public void T00220_OneWayDebatchAggregateMessageSendPortMessageTimeout()
        {
            BizUnit.BizUnit but = new BizUnit.BizUnit(@"..\..\..\ESB.Extensions.Tests\TestCases\T00220-OneWay-Debatch-Aggregate-MessageSendPort-MessageTimeout.bizunit.xml");
            but.RunTest();
        }

        [TestMethod]
        public void T00230_OneWayDebatchAggregateMessageSendPortBatchTimeout()
        {
            BizUnit.BizUnit but = new BizUnit.BizUnit(@"..\..\..\ESB.Extensions.Tests\TestCases\T00230-OneWay-Debatch-Aggregate-MessageSendPort-BatchTimeout.bizunit.xml");
            but.RunTest();
        }

        [TestMethod]
        public void T00300_OneWayDebatchResequenceAggregateMessageSendPort()
        {
            BizUnit.BizUnit but = new BizUnit.BizUnit(@"..\..\..\ESB.Extensions.Tests\TestCases\T00300-OneWay-Debatch-Resequence-Aggregate-MessageSendPort.bizunit.xml");
            but.RunTest();
        }

        [TestMethod]
        public void T00310_OneWayDebatchResequenceAggregateMessageSendPort5Batches()
        {
            BizUnit.BizUnit but = new BizUnit.BizUnit(@"..\..\..\ESB.Extensions.Tests\TestCases\T00310-OneWay-Debatch-Resequence-Aggregate-MessageSendPort-5-Batches.bizunit.xml");
            but.RunTest();
        }

        [TestMethod]
        public void T00350_OneWayDebatchEsbBatchDbResequenceAggregateMessageSendPort()
        {
            BizUnit.BizUnit but = new BizUnit.BizUnit(@"..\..\..\ESB.Extensions.Tests\TestCases\T00350-OneWay-Debatch-EsbBatchDb-Resequence-Aggregate-MessageSendPort.bizunit.xml");
            but.RunTest();
        }

    }
}
