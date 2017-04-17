namespace _01_SystemSplit
{
    using Enumerations;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public abstract class HardwareComponent : Component
    {
        private readonly HardwareType type;
        private readonly int maxCapacity;
        private readonly int maxMemory;
        private List<SoftwareComponent> softwareComponents;

        public HardwareComponent(string name, HardwareType type, int maxCapacity, int maxMemory) 
            : base(name)
        {
            this.type = type;
            this.maxCapacity = maxCapacity;
            this.maxMemory = maxMemory;
            
            this.softwareComponents = new List<SoftwareComponent>();
        }

        public HardwareType Type => this.type;

        public int MaxCapacity => this.maxCapacity;

        public int MaxMemory => this.maxMemory;

        public int UsedCapacity
        {
            get { return this.softwareComponents.Sum(x => x.CapacityConsumption); }
        }

        public int UsedMemory
        {
            get { return this.softwareComponents.Sum(x => x.MemoryConsumption); }
        }

        public int ExpressSoftwareCount
        {
            get { return this.softwareComponents.Count(x => x.Type == SoftwareType.Express); }
        }

        public int LightSoftwareCount
        {
            get { return this.softwareComponents.Count(x => x.Type == SoftwareType.Light); }
        }

        public void AddSoftwareComponent(SoftwareComponent softwareComponent)
        {
            if (this.UsedCapacity + softwareComponent.CapacityConsumption <= this.MaxCapacity
             && this.UsedMemory + softwareComponent.MemoryConsumption <= this.MaxMemory)
            {
                this.softwareComponents.Add(softwareComponent);
            }
        }

        public void RemoveSoftwareComponent(string componentName)
        {
            if (this.softwareComponents.Exists(x => x.Name == componentName))
            {
                this.softwareComponents.Remove(this.softwareComponents.Find(x => x.Name == componentName));
            }
        }

        public string GetSoftwareComponentsString()
        {
            StringBuilder builder = new StringBuilder();

            if (this.softwareComponents.Count == 0)
            {
                return "None";
            }

            if (this.softwareComponents.Count == 1)
            {
                builder.Append($"{this.softwareComponents[0].Name}");
                return builder.ToString();
            }

            builder.Append($"{this.softwareComponents[0].Name}");

            for (int i = 1; i < this.softwareComponents.Count; i++)
            {
                builder.Append($", {this.softwareComponents[i].Name}");
            }

            return builder.ToString();
        }
    }
}