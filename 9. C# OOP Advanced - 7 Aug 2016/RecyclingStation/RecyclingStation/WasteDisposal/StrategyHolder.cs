namespace RecyclingStation.WasteDisposal
{
    using System;
    using System.Collections.Generic;
    using Attributes;
    using RecyclingStation.WasteDisposal.Interfaces;

    public class StrategyHolder : IStrategyHolder
    {
        private readonly IDictionary<Type, IGarbageDisposalStrategy> strategies;

        public StrategyHolder()
        {
            this.strategies = new Dictionary<Type, IGarbageDisposalStrategy>();
        }

        public IReadOnlyDictionary<Type,IGarbageDisposalStrategy> GetDisposalStrategies
        {
            get { return (IReadOnlyDictionary<Type, IGarbageDisposalStrategy>)this.strategies; }
        }

        public bool AddStrategy(Type disposableAttribute, IGarbageDisposalStrategy strategy)
        {
            if (!disposableAttribute.IsSubclassOf(typeof(DisposableAttribute)))
            {
                throw new ArgumentException($"'{disposableAttribute}' is not a type derived from DisposableAttribute");
            }
            
            this.strategies.Add(disposableAttribute, strategy);
            return true;
        }

        public bool RemoveStrategy(Type disposableAttribute)
        {
            if (!disposableAttribute.IsSubclassOf(typeof(DisposableAttribute)))
            {
                throw new ArgumentException($"'{disposableAttribute}' is not a type derived from DisposableAttribute");
            }

            this.strategies.Remove(disposableAttribute);
            return true;
        }
    }
}
