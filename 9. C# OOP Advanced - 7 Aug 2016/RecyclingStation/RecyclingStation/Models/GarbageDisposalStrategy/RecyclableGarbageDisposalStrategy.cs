namespace RecyclingStation.Models.GarbageDisposalStrategy
{
    using GarbageProcessingData;
    using WasteDisposal.Interfaces;

    public class RecyclableGarbageDisposalStrategy : IGarbageDisposalStrategy
    {
        public IProcessingData ProcessGarbage(IWaste garbage)
        {
            double totalGarbageVolume = garbage.Weight * garbage.VolumePerKg;
            double energyProduced = 0;
            double energyUsed = 0.5 * totalGarbageVolume;
            double capitalEarned = 400 * garbage.Weight;
            double capitalUsed = 0;

            IProcessingData processingData = new GarbageProcessingData(
                energyProduced - energyUsed,
                capitalEarned - capitalUsed);

            return processingData;
        }
    }
}
