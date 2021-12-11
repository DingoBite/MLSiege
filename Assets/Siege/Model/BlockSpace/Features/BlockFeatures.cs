using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.General.Interfaces;

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

        public void FeatureSet(int index, int value)
        {
            _featuresValues[index] = value;
        }

        public void FeatureChange(int index, int value)
        {
            _featuresValues[index] += value;
        }

        public void FeatureChange(int index, int value, IModifier modifier)
        {
            _featuresValues[index] += modifier.ModifyChangeValue(value);
        }

        public void FeatureChange(int index, int value, IEnumerable<IModifier> modifiers)
        {
            foreach (var modifier in modifiers)
            {
                FeatureChange(index, value, modifier);
            }
        }
    }
}