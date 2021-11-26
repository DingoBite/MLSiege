﻿using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockFabric
    {
        public AbstractBlock MakeBlock(int id, BlockFeatures blockFeatures);
        public AbstractBlock MakeBlock(int id, BlockInfo blockInfo);
    }
}