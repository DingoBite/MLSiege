using System.Collections.Generic;
using Assets.Siege.Model.General.Interfaces;

namespace Assets.Siege.Model.ObjectFeatures.Interfaces
{
    public interface IFeatures
    {
        public void CommitFeatureChange(int feature, int value, IModifier modifier);

        public void CommitFeatureChange(int feature, int value, IEnumerable<IModifier> modifiers);
    }
}