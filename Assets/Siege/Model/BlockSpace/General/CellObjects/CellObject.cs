namespace Assets.Siege.Model.BlockSpace.General.CellObjects
{
    public class CellObject<TFeatures>
    {
        public readonly TFeatures Features;

        protected CellObject(TFeatures features)
        {
            Features = features;
        }
    }
}