namespace Assets.Siege.Model.General.BlockSpaceObjects
{
    public class BlockSpaceObject<TFeatures>
    {
        public readonly TFeatures Features;

        public BlockSpaceObject(TFeatures features)
        {
            Features = features;
        }
    }
}