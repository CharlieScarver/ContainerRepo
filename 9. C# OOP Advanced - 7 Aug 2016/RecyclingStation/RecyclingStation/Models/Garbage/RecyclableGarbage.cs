namespace RecyclingStation.Models.Garbage
{
    using Enumerations;
    using WasteDisposal.Attributes;

    [Recyclable]
    public class RecyclableGarbage : Garbage
    {
        public RecyclableGarbage(string name, double volumePerKg, double weight)
            : base(name, volumePerKg, weight, GarbageTypes.Recyclable)
        {
        }
    }
}
