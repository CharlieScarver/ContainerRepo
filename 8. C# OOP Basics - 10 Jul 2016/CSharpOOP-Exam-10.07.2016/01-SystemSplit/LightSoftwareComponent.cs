namespace _01_SystemSplit
{
    using Enumerations;

    public class LightSoftwareComponent : SoftwareComponent
    {
        private const double CapacityConsumptionIncreaseCoef = 0.5;
        private const double MemoryConsumptionDecreaseCoef = 0.5;

        public LightSoftwareComponent(string name, int capacityConsumption, int memoryConsumption) 
            : base(name, SoftwareType.Light, capacityConsumption + (int)(capacityConsumption * CapacityConsumptionIncreaseCoef), memoryConsumption - (int)(memoryConsumption * MemoryConsumptionDecreaseCoef))
        {

        }
    }
}
