using System.Collections.Generic;
using Assets.Siege.Model.General.Interfaces;

namespace Assets.Siege.Model.ObjectFeatures.FeaturesInstances
{
    public class CellableObjectFeatures: AbstractFeatures
    {
        public CellableObjectFeatures(IEnumerable<int> featureValues) : base(featureValues)
        {
        }

        public override bool CommitFeatureChange(int value, int feature, IModifier modifier)
        {
            throw new System.NotImplementedException();
        }

        public override bool CommitFeatureChange(int value, int feature, IEnumerable<IModifier> modifiers)
        {
            throw new System.NotImplementedException();
        }
    }
}