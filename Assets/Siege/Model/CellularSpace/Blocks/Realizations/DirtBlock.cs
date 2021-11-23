using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks;

namespace Assets.Siege.Model.CellularSpace.Blocks.Realizations
{
    public class DirtBlock: AbstractBlock
    {
        public DirtBlock(int id, BlockFeatures features) : base(id, features)
        {
        }

        public override bool CommitAction(IBlockSpace blockContext)
        {
            throw new System.NotImplementedException();
        }
    }
}