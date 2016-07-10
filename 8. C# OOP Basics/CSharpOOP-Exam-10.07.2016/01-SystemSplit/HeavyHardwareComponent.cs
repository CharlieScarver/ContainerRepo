namespace _01_SystemSplit
{
    using Enumerations;

    public class HeavyHardwareComponent : HardwareComponent
    {
        private const double CapacityIncreaseCoef = 1;
        private const double MemoryDecreaseCoef = 0.25;

        public HeavyHardwareComponent(string name, int maxCapacity, int maxMemory) 
            : base(name, HardwareType.Heavy, maxCapacity + (int)(maxCapacity * CapacityIncreaseCoef), maxMemory - (int)(maxMemory * MemoryDecreaseCoef))
        {

        }
    }
}
