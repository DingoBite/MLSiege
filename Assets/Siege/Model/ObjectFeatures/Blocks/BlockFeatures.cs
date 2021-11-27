using System.Collections.Generic;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.ObjectFeatures.Blocks
{
    public class BlockFeatures: IFeatures
    {
        public readonly BlockType BlockType;
        public readonly BlockSolidity BlockSolidity;

        public BlockFeatures(BlockType blockType, BlockSolidity blockSolidity)
        {
            BlockType = blockType;
            BlockSolidity = blockSolidity;
        }

        public BlockFeatures(BlockInfo blockInfo) : this(blockInfo.BlockType, blockInfo.BlockSolidity)
        {
        }

        public virtual bool CommitFeatureChange(int value, int feature, IModifier modifier)
        {
            throw new System.NotImplementedException();
        }

        public virtual bool CommitFeatureChange(int value, int feature, IEnumerable<IModifier> modifiers)
        {
            throw new System.NotImplementedException();
        }
    }
}