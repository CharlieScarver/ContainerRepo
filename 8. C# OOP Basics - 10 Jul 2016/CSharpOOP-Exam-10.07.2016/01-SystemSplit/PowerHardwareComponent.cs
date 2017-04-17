namespace _01_SystemSplit
{
    using Enumerations;

    public class PowerHardwareComponent : HardwareComponent
    {
        private const double CapacityDecreaseCoef = 0.75;
        private const double MemoryIncreaseCoef = 0.75;

        public PowerHardwareComponent(string name, int maxCapacity, int maxMemory) 
            : base(name, HardwareType.Power, maxCapacity - (int)(maxCapacity * CapacityDecreaseCoef), maxMemory + (int)(maxMemory * MemoryIncreaseCoef))
        {

        }
    }
}
