namespace _01_SystemSplit
{
    using Enumerations;

    public class ExpressSoftwareComponent : SoftwareComponent
    {
        private const double MemoryConsumptionIncreaseCoef = 1;

        public ExpressSoftwareComponent(string name, int capacityConsumption, int memoryConsumption) 
            : base(name, SoftwareType.Express, capacityConsumption, memoryConsumption + (int)(memoryConsumption * MemoryConsumptionIncreaseCoef))
        {
          
        }
    }
}
