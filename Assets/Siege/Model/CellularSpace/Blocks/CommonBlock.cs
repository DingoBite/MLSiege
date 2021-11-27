using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public class CommonBlock
    {
        public readonly int Id;
        public readonly BlockFeatures Features;

        public CommonBlock(int id, BlockFeatures features)
        {
            Id = id;
            Features = features;
        }

        public virtual bool CommitAction(IBlockSpace blockContext)
        {
            throw new System.NotImplementedException();
        }
    }
}