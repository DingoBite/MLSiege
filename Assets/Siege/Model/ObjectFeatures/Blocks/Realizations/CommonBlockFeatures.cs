using System.Collections.Generic;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.General.Interfaces;

namespace Assets.Siege.Model.ObjectFeatures.Blocks.Realizations
{
    public class CommonBlockFeatures: BlockFeatures
    {
        public CommonBlockFeatures(BlockType blockType) : base(blockType)
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