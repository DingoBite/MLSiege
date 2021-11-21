using System.Collections.Generic;
using Assets.Siege.Model.General.Interfaces;

namespace Assets.Siege.Model.ObjectFeatures.Interfaces
{
    public interface IFeatureChangeCommitter
    {
        public bool CommitFeatureChange(int value, int feature, IModifier modifier);
        public bool CommitFeatureChange(int value, int feature, IEnumerable<IModifier> modifiers);
    }
}