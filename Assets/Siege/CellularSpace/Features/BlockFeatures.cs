using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Blocks.Enums;

namespace Assets.Siege.CellularSpace.Features
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