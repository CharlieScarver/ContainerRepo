namespace RecyclingStation.Models.GarbageDisposalStrategy
{
    using GarbageProcessingData;
    using WasteDisposal.Interfaces;

    public class StorableGarbageDisposalStrategy : IGarbageDisposalStrategy
    {
        public IProcessingData ProcessGarbage(IWaste garbage)
        {
            double totalGarbageVolume = garbage.Weight * garbage.VolumePerKg;
            double energyProduced = 0;
            double energyUsed = 0.13 * totalGarbageVolume;
            double capitalEarned = 0;
            double capitalUsed = 0.65 * totalGarbageVolume;

            IProcessingData processingData = new GarbageProcessingData(
                energyProduced - energyUsed,
                capitalEarned - capitalUsed);

            return processingData;
        }
    }
}
