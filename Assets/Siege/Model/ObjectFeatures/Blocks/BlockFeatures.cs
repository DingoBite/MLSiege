using System.Collections.Generic;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Interfaces;

namespace Assets.Siege.Model.ObjectFeatures.Blocks
{
    public abstract class BlockFeatures: IFeatures
    {
        public readonly BlockType BlockType;

        public BlockFeatures(BlockType blockType)
        {
            BlockType = blockType;
        }

        public abstract bool CommitFeatureChange(int value, int feature, IModifier modifier);

        public abstract bool CommitFeatureChange(int value, int feature, IEnumerable<IModifier> modifiers);
    }
}