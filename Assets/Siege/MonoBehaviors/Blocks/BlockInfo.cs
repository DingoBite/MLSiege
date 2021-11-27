using Assets.Siege.Model.General.Enums;

namespace Assets.Siege.MonoBehaviors.Blocks
{
    public class BlockInfo
    {
        public readonly BlockType BlockType;
        public readonly BlockSolidity BlockSolidity;

        public BlockInfo(BlockType blockType, BlockSolidity blockSolidity)
        {
            BlockType = blockType;
            BlockSolidity = blockSolidity;
        }
    }
}