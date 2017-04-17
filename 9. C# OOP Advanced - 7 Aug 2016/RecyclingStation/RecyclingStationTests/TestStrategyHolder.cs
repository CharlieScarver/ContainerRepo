namespace RecyclingStationTests
{
    using System;
    using System.CodeDom;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using RecyclingStation.Models.Garbage;
    using RecyclingStation.Models.GarbageDisposalStrategy;
    using RecyclingStation.WasteDisposal;
    using RecyclingStation.WasteDisposal.Attributes;
    using RecyclingStation.WasteDisposal.Interfaces;

    [TestClass]
    public class TestStrategyHolder
    {
        private IStrategyHolder strategyHolder;

        [TestInitialize]
        public void SetUp()
        {
            this.strategyHolder = new StrategyHolder();
        }

        [TestMethod]
        public void TestAddStrategy_ValidStrategy_ExpectStrategyToBeAdded()
        {
            Type burnableAttributeType = typeof (BurnableAttribute);
            IGarbageDisposalStrategy strategy = new BurnableGarbageDisposalStrategy();
            this.strategyHolder.AddStrategy(burnableAttributeType, strategy);

            var firstStrategy = this.strategyHolder.GetDisposalStrategies.ToArray()[0];

            Assert.AreEqual(burnableAttributeType, firstStrategy.Key);
            Assert.AreEqual(strategy, firstStrategy.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddStrategy_ValidStrategyKeyExists_ExpectException()
        {
            Type burnableAttributeType = typeof(BurnableAttribute);
            IGarbageDisposalStrategy strategy = new BurnableGarbageDisposalStrategy();

            this.strategyHolder.AddStrategy(burnableAttributeType, strategy);
            this.strategyHolder.AddStrategy(burnableAttributeType, strategy);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddStrategy_ValidStrategyInvalidKey_ExpectException()
        {
            Type invalidAttributeType = typeof(BurnableGarbage);
            IGarbageDisposalStrategy strategy = new BurnableGarbageDisposalStrategy();

            this.strategyHolder.AddStrategy(invalidAttributeType, strategy);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRemoveStrategy_InvalidKey_ExpectException()
        {
            Type invalidAttributeType = typeof(BurnableGarbage);
            
            this.strategyHolder.RemoveStrategy(invalidAttributeType);
        }
    }
}
