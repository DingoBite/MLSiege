using System.Collections.Generic;
using Assets.Siege.CellularSpace.General.Interfaces;

namespace Assets.Siege.CellularSpace.Features
{
    public abstract class AbstractFeatures
    {
        protected readonly List<int> _featuresValues;

        protected AbstractFeatures()
        {
            _featuresValues = new List<int>();
        }

        public abstract int this[int i] { get; }

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