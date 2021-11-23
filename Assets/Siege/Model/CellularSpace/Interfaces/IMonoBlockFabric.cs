﻿using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IMonoBlockFabric
    {
        public MonoBlock MakeMonoBlock(int id, Vector3 position, AbstractBlock block);
    }
}