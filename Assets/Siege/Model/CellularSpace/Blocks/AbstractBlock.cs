using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public abstract class AbstractBlock
    {
        public readonly int Id;
        public readonly BlockFeatures Features;

        public AbstractBlock(int id, BlockFeatures features)
        {
            Id = id;
            Features = features;
        }

        public abstract bool CommitAction(IBlockSpace blockContext);
    }
}