﻿using System.Collections.Generic;
using Assets.Siege.Model.CellularSpace.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockSpaceContext
    {
        public IEnumerable<AbstractBlock> GetCustomers();
        public bool GetBlock(int id, out AbstractBlock block);
        public bool GetBlock(Vector3Int coords, out AbstractBlock block);
    }
}