using System;
using System.Collections.Generic;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.General.Interfaces;

namespace Assets.Siege.Model.ObjectFeatures.Blocks.Realizations
{
    public class NullBlockFeatures: BlockFeatures
    {
        public NullBlockFeatures() : base(BlockType.Null)
        {
        }

        public override bool CommitFeatureChange(int value, int feature, IModifier modifier)
        {
            throw new Exception("Try to commit change on null features");
        }

        public override bool CommitFeatureChange(int value, int feature, IEnumerable<IModifier> modifiers)
        {
            throw new Exception("Try to commit change on null features");
        }
    }
}