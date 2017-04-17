namespace RecyclingStationTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RecyclingStation.Models.GarbageDisposalStrategy;
    using RecyclingStation.WasteDisposal;
    using RecyclingStation.WasteDisposal.Interfaces;

    [TestClass]
    public class TestGarbageProcessor
    {
        private IStrategyHolder strategyHolder;
        private IGarbageProcessor garbageProcessor;

        [TestInitialize]
        public void SetUp()
        {
            this.strategyHolder = new StrategyHolder();
            this.garbageProcessor = new GarbageProcessor(this.strategyHolder);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestProcessWaste_NoDisposableAttribute_ExpectException()
        {
            var invalidClass = new Waste();

            this.garbageProcessor.ProcessWaste(invalidClass);
        }
    }
}
