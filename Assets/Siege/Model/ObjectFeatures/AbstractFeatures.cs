using System.Collections.Generic;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Interfaces;

namespace Assets.Siege.Model.ObjectFeatures
{
    public abstract class AbstractFeatures: IFeatureChangeCommitter
    {
        private readonly List<int> _featureValues;

        public AbstractFeatures(IEnumerable<int> featureValues)
        {
            _featureValues = (List<int>) featureValues;
        }
        public virtual List<int> FeatureDescription => _featureValues;
        public int ValueOf(int feature) => _featureValues[feature];
        public abstract bool CommitFeatureChange(int value, int feature, IModifier modifier);

        public abstract bool CommitFeatureChange(int value, int feature, IEnumerable<IModifier> modifiers);
    }
}