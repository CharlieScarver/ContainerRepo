namespace _01_SystemSplit
{
    using Enumerations;

    public abstract class SoftwareComponent : Component
    {
        private readonly SoftwareType type;
        private readonly int capacityConsumption;
        private readonly int memoryConsumption;

        protected SoftwareComponent(string name, SoftwareType type, int capacityConsumption, int memoryConsumption)
            : base(name)
        {
            this.type = type;
            this.capacityConsumption = capacityConsumption;
            this.memoryConsumption = memoryConsumption;
        }

        public SoftwareType Type => this.type;

        public int CapacityConsumption => this.capacityConsumption;

        public int MemoryConsumption => this.memoryConsumption;
    }
}