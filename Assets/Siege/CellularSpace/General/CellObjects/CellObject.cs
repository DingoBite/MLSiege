using Assets.Siege.CellularSpace.Features;

namespace Assets.Siege.CellularSpace.General.CellObjects
{
    public class CellObject<TFeatures> where TFeatures : AbstractFeatures
    {
        public readonly TFeatures Features;

        protected CellObject(TFeatures features)
        {
            Features = features;
        }
    }
}