using Assets.Siege.Model.Agents;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public delegate bool BlockBehavior(SiegeAgent sender, Block committer, IBlockSpace blockSpace, ActionType actionType);
    public class BlockInfo
    {
        public readonly BlockType BlockType;
        public readonly BlockSolidity BlockSolidity;
        public readonly BlockBehavior BlockBehavior;

        public BlockInfo(BlockType blockType, BlockSolidity blockSolidity, BlockBehavior blockBehavior)
        {
            BlockType = blockType;
            BlockSolidity = blockSolidity;
            BlockBehavior = blockBehavior;
        }
    }
}