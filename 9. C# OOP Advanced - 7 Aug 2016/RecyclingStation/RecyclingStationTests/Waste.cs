namespace RecyclingStationTests
{
    using RecyclingStation.WasteDisposal.Interfaces;

    public class Waste : IWaste
    {
        public string Name { get; }
        public double VolumePerKg { get; }
        public double Weight { get; }
    }
}
