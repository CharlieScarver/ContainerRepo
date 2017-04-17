namespace RecyclingStation.Models.Garbage
{
    using Enumerations;
    using WasteDisposal.Attributes;

    [Burnable]
    public class BurnableGarbage : Garbage
    {
        public BurnableGarbage(string name, double volumePerKg, double weight)
            : base(name, volumePerKg, weight, GarbageTypes.Burnable)
        {
        }
    }
}
