using Assets.Siege.Model.BlockSpace.Features;

namespace Assets.Siege.Model.BlockSpace.General.CellObjects
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