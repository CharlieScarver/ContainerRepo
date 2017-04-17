namespace RecyclingStation.Core
{
    using System;
    using System.Linq;
    using IO.Readers;
    using IO.Writers;
    using Models.Garbage;
    using Models.GarbageDisposalStrategy;
    using WasteDisposal;
    using WasteDisposal.Interfaces;

    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private IStrategyHolder strategyHolder;
        private IGarbageProcessor garbageProcessor;
        private double energy;
        private double capital;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            this.strategyHolder = new StrategyHolder();
            this.garbageProcessor = new GarbageProcessor(this.strategyHolder);
            this.energy = 0;
            this.capital = 0;
        }

        public void Run()
        {
            string input = reader.ReadLine();

            while (input != "TimeToRecycle")
            {
                string[] inputParams = input.Trim().Split(' ');

                switch (inputParams[0])
                {
                    case "ProcessGarbage":
                        string[] commandParams = inputParams[1].Split('|');

                        Garbage garbage = null;
                        Type garbageAttributeType;

                        double volumePerKg = Convert.ToDouble(commandParams[2]);
                        double weight = Convert.ToDouble(commandParams[1]);

                        switch (commandParams[3])
                        {
                            case "Burnable":
                                garbage = new BurnableGarbage(commandParams[0], volumePerKg, weight);
                                garbageAttributeType = garbage.GetType().GetCustomAttributes(false).First().GetType();

                                if (!strategyHolder.GetDisposalStrategies.ContainsKey(garbageAttributeType))
                                {
                                    strategyHolder.AddStrategy(garbageAttributeType, new BurnableGarbageDisposalStrategy());
                                }
                                break;
                            case "Recyclable":
                                garbage = new RecyclableGarbage(commandParams[0], volumePerKg, weight);
                                garbageAttributeType = garbage.GetType().GetCustomAttributes(false).First().GetType();

                                if (!strategyHolder.GetDisposalStrategies.ContainsKey(garbageAttributeType))
                                {
                                    strategyHolder.AddStrategy(garbageAttributeType, new RecyclableGarbageDisposalStrategy());
                                }
                                break;
                            case "Storable":
                                garbage = new StorableGarbage(commandParams[0], volumePerKg, weight);
                                garbageAttributeType = garbage.GetType().GetCustomAttributes(false).First().GetType();

                                if (!strategyHolder.GetDisposalStrategies.ContainsKey(garbageAttributeType))
                                {
                                    strategyHolder.AddStrategy(garbageAttributeType, new StorableGarbageDisposalStrategy());
                                }
                                break;
                        }
                        
                        IProcessingData result = garbageProcessor.ProcessWaste(garbage);

                        this.energy += result.EnergyBalance;
                        this.capital += result.CapitalBalance;

                        writer.WriteLine($"{garbage.Weight:F2} kg of {garbage.Name} successfully processed!");
                        break;

                    case "Status":
                        writer.WriteLine($"Energy: {this.energy:F2} Capital: {this.capital:F2}");
                        break;
                }

                input = reader.ReadLine();
            }

        }
    }
}
