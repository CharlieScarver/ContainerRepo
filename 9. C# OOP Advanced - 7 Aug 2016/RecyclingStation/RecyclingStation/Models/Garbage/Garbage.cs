namespace RecyclingStation.Models.Garbage
{
    using System;
    using Enumerations;
    using WasteDisposal.Attributes;
    using WasteDisposal.Interfaces;

    [Disposable]
    public abstract class Garbage : IWaste
    {
        private string name;
        private double volumePerKg;
        private double weight;

        protected Garbage(string name, double volumePerKg, double weight, GarbageTypes type)
        {
            this.Name = name;
            this.VolumePerKg = volumePerKg;
            this.Weight = weight;
            this.Type = type;
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Name should not be null or empty");
                }

                this.name = value;
            }
        }

        public double VolumePerKg
        {
            get { return this.volumePerKg; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("VolumePerKg should not be zero or negative");
                }

                this.volumePerKg = value;
            }
        }

        public double Weight
        {
            get { return this.weight; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Weight should not be zero or negative");
                }

                this.weight = value;
            }
        }

        public GarbageTypes Type { get; }
    }
}
