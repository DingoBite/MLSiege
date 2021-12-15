using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.General.Interfaces;

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

    }
}