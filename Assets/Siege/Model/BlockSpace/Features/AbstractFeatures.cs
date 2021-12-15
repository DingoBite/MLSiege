using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.General.Interfaces;

namespace Assets.Siege.Model.BlockSpace.Features
{
    public abstract class AbstractFeatures
    {
        protected readonly List<int> _featuresValues;

        public AbstractFeatures()
        {
            _featuresValues = new List<int>();
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