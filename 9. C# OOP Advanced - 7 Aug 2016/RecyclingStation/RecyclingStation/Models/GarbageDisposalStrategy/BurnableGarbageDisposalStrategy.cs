namespace RecyclingStation.Models.GarbageDisposalStrategy
{
    using GarbageProcessingData;
    using WasteDisposal.Interfaces;

    public class BurnableGarbageDisposalStrategy : GarbageDisposalStrategy
    {
        public override IProcessingData ProcessGarbage(IWaste garbage)
        {
            double totalGarbageVolume = garbage.Weight*garbage.VolumePerKg;
            double energyProduced = 1 * totalGarbageVolume;
            double energyUsed = 0.2 * totalGarbageVolume;
            double capitalEarned = 0;
            double capitalUsed = 0;

            IProcessingData processingData = new GarbageProcessingData(
                energyProduced - energyUsed, 
                capitalEarned - capitalUsed);

            return processingData;
        }
    }
}
