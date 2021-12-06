using Assets.Siege.Model.Agents;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public class Block
    {
        public readonly int Id;
        public readonly BlockFeatures Features;
        private readonly BlockBehavior _blockBehavior;

        public Block(int id, BlockInfo blockInfo)
        {
            Id = id;
            Features = new BlockFeatures(blockInfo);
            _blockBehavior = blockInfo.BlockBehavior;
        }

        public bool CommitAction(SiegeAgent sender, IBlockSpace blockSpace, ActionType actionType) 
            => _blockBehavior(sender, this, blockSpace, actionType);
    }
}