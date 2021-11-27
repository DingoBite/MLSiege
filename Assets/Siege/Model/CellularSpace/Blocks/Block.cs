using System;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.ObjectFeatures.Blocks;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public class Block
    {
        public readonly int Id;
        public readonly BlockFeatures Features;
        public readonly Func<int, IBlockSpace, ActionType, bool> BlockBehavior;

        public Block(int id, BlockFeatures features)
        {
            Id = id;
            Features = features;
        }
    }
}