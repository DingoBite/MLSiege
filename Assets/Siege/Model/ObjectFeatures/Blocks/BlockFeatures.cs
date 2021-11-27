using System;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.ObjectFeatures.Blocks
{
    public class BlockFeatures
    {
        public readonly BlockType BlockType;
        public readonly BlockSolidity BlockSolidity;
        public readonly Action<int, int, IModifier> FeaturesBehavior;

        public BlockFeatures(BlockType blockType, BlockSolidity blockSolidity)
        {
            BlockType = blockType;
            BlockSolidity = blockSolidity;
        }

        public BlockFeatures(BlockInfo blockInfo) : this(blockInfo.BlockType, blockInfo.BlockSolidity)
        {
        }
    }
}