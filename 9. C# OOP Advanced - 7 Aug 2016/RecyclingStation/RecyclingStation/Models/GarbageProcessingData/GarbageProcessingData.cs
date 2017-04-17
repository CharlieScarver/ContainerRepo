namespace RecyclingStation.Models.GarbageProcessingData
{
    using WasteDisposal.Interfaces;

    public class GarbageProcessingData : IProcessingData
    {
        private double energyBalance;
        private double capitalBalance;

        public GarbageProcessingData(double energyBalance, double capitalBalance)
        {
            this.EnergyBalance = energyBalance;
            this.CapitalBalance = capitalBalance;
        }

        public double EnergyBalance
        {
            get { return this.energyBalance; }
            protected set { this.energyBalance = value; }
        }

        public double CapitalBalance
        {
            get { return this.capitalBalance; }
            protected set { this.capitalBalance = value; }
        }
        
    }
}
