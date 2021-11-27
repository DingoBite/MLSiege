﻿using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.CellularSpace.Fabrics
{
    public class BlockFabric: IBlockFabric
    {
        public BlockFabric() { }

        public CommonBlock Make(int id, BlockFeatures blockFeatures)
        {
            return new CommonBlock(id, blockFeatures);
        }

        public CommonBlock Make(int id, BlockInfo blockInfo)
        {
            return new CommonBlock(id, new BlockFeatures(blockInfo));
        }
    }
}