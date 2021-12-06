using System.Collections.Generic;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.View.Blocks;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public class BlockFeatures
    {
        public readonly BlockType BlockType;
        public readonly BlockSolidity BlockSolidity;

        private List<int> _featuresValues;

        public BlockFeatures(BlockInfo blockInfo)
        { 
            BlockType = blockInfo.BlockType;
            BlockSolidity = blockInfo.BlockSolidity;
        }

        public void CommitFeatureChange(int feature, int value, IModifier modifier)
        {
            _featuresValues[feature] += modifier.ModifyChangeValue(value);
        }

        public void CommitFeatureChange(int feature, int value, IEnumerable<IModifier> modifiers)
        {
            foreach (var modifier in modifiers)
            {
                CommitFeatureChange(feature, value, modifier);
            }
        }
    }
}