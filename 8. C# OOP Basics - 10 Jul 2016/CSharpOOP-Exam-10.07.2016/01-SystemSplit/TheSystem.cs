namespace _01_SystemSplit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Enumerations;

    public class TheSystem
    {
        private List<HardwareComponent> hardwareComponents;
        private List<HardwareComponent> dump; 

        public TheSystem()
        {
            this.hardwareComponents = new List<HardwareComponent>();
            this.dump = new List<HardwareComponent>();
        }

        public void RegisterPowerHardware(string name, int capacity, int memory)
        {
            PowerHardwareComponent hardwareComponent = new PowerHardwareComponent(name, capacity, memory);
            this.hardwareComponents.Add(hardwareComponent);
        }

        public void RegisterHeavyHardware(string name, int capacity, int memory)
        {
            HeavyHardwareComponent hardwareComponent = new HeavyHardwareComponent(name, capacity, memory);
            this.hardwareComponents.Add(hardwareComponent);
        }

        public void RegisterExpressSoftware(string hardwareCompName, string name, int capacityCons, int memoryCons)
        {
            if (this.hardwareComponents.Exists(x => x.Name == hardwareCompName))
            {
                ExpressSoftwareComponent newComponent = new ExpressSoftwareComponent(name, capacityCons, memoryCons);
                this.hardwareComponents.Find(x => x.Name == hardwareCompName).AddSoftwareComponent(newComponent);
            }
        }

        public void RegisterLightSoftware(string hardwareCompName, string name, int capacityCons, int memoryCons)
        {
            if (this.hardwareComponents.Exists(x => x.Name == hardwareCompName))
            {
                LightSoftwareComponent newComponent = new LightSoftwareComponent(name, capacityCons, memoryCons);
                this.hardwareComponents.Find(x => x.Name == hardwareCompName).AddSoftwareComponent(newComponent);
            }
        }

        public void ReleaseSoftwareComponent(string hardwareCompName, string softwareCompName)
        {
            if (this.hardwareComponents.Exists(x => x.Name == hardwareCompName))
            {
                this.hardwareComponents.Find(x => x.Name == hardwareCompName).RemoveSoftwareComponent(softwareCompName);
            }
        }

        public void Analyze()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("System Analysis");
            builder.AppendLine($"Hardware Components: {this.hardwareComponents.Count}");
            builder.AppendLine($"Software Components: {this.hardwareComponents.Sum(x => x.ExpressSoftwareCount + x.LightSoftwareCount)}");
            builder.AppendLine($"Total Operational Memory: {this.hardwareComponents.Sum(x => x.UsedMemory)} / {this.hardwareComponents.Sum(x => x.MaxMemory)}");
            builder.Append($"Total Capacity Taken: {this.hardwareComponents.Sum(x => x.UsedCapacity)} / {this.hardwareComponents.Sum(x => x.MaxCapacity)}");

            Console.WriteLine(builder.ToString());
        }

        public void SystemSplit()
        {
            var sorted = this.hardwareComponents.OrderBy(x => x.Type);

            StringBuilder builder = new StringBuilder();

            foreach (HardwareComponent hardwareComponent in sorted)
            {
                builder.AppendLine($"Hardware Component - {hardwareComponent.Name}");
                builder.AppendLine($"Express Software Components - {hardwareComponent.ExpressSoftwareCount}");
                builder.AppendLine($"Light Software Components - {hardwareComponent.LightSoftwareCount}");
                builder.AppendLine($"Memory Usage: {hardwareComponent.UsedMemory} / {hardwareComponent.MaxMemory}");
                builder.AppendLine($"Capacity Usage: {hardwareComponent.UsedCapacity} / {hardwareComponent.MaxCapacity}");
                builder.AppendLine($"Type: {hardwareComponent.Type}");
                string softwareComponentsString = hardwareComponent.GetSoftwareComponentsString();
                builder.AppendLine($"Software Components: {softwareComponentsString}");
            }

            Console.WriteLine(builder.ToString());
        }

        public void Dump(string hardwareCompName)
        {
            if (this.hardwareComponents.Exists(x => x.Name == hardwareCompName))
            {
                HardwareComponent componentToDump = this.hardwareComponents.Find(x => x.Name == hardwareCompName);
                this.dump.Add(componentToDump);
                this.hardwareComponents.Remove(componentToDump);
            }
        }

        public void Restore(string hardwareCompName)
        {
            if (this.dump.Exists(x => x.Name == hardwareCompName))
            {
                HardwareComponent componentToRestore = this.dump.Find(x => x.Name == hardwareCompName);
                this.hardwareComponents.Add(componentToRestore);
                this.dump.Remove(componentToRestore);
            }
        }

        public void Destroy(string hardwareCompName)
        {
            if (this.dump.Exists(x => x.Name == hardwareCompName))
            {
                this.dump.Remove(this.dump.Find(x => x.Name == hardwareCompName));
            }
        }

        public void DumpAnalyze()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Dump Analysis");
            builder.AppendLine($"Power Hardware Components: {this.dump.Count(x => x.Type == HardwareType.Power)}");
            builder.AppendLine($"Heavy Hardware Components: {this.dump.Count(x => x.Type == HardwareType.Heavy)}");
            builder.AppendLine($"Express Software Components: {this.dump.Sum(x => x.ExpressSoftwareCount)}");
            builder.AppendLine($"Light Software Components: {this.dump.Sum(x => x.LightSoftwareCount)}");
            builder.AppendLine($"Total Dumped Memory: {this.dump.Sum(x => x.UsedMemory)}");
            builder.Append($"Total Dumped Capacity: {this.dump.Sum(x => x.UsedCapacity)}");

            Console.WriteLine(builder.ToString());
        }
    }
}
