using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.General.Interfaces;

namespace Assets.Siege.Model.BlockSpace.Features
{
    public class BlockFeatures
    {
        public readonly BlockType BlockType;
        public readonly BlockSolidity BlockSolidity;

        private readonly List<int> _featuresValues;

        public BlockFeatures(BlockData blockData)
        {
            _featuresValues = new List<int>();
            BlockType = blockData.BlockType;
            BlockSolidity = blockData.BlockSolidity;
        }

        public void CommitFeatureChange(int feature, int value)
        {
            _featuresValues[feature] += value;
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