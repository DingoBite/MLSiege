using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;

namespace Assets.Siege.Model.BlockSpace.Features
{
    public class BlockFeatures : AbstractFeatures
    {
        public readonly BlockType BlockType;
        public readonly BlockSolidity BlockSolidity;
        public BlockFeatures(BlockData blockData)
        {
            BlockType = blockData.BlockType;
            BlockSolidity = blockData.BlockSolidity;
        }

        public override int this[int i] => _featuresValues[i];
    }
}