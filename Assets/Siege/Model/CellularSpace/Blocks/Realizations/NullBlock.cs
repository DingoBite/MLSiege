using System;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks.Realizations;

namespace Assets.Siege.Model.CellularSpace.Blocks.Realizations
{
    public class NullBlock: AbstractBlock
    {
        public NullBlock(int id) : base(id, new NullBlockFeatures())
        {
        }

        public override bool CommitAction(IBlockSpace blockContext)
        {
            throw new Exception("Try to commit action on null block");
        }
    }
}