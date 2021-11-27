using System.Collections.Generic;
using Assets.Siege.Model.CellularSpace.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockSpaceContext
    {
        public IEnumerable<CommonBlock> GetBlocks();
        public bool GetBlock(int id, out CommonBlock block);
        public bool GetBlock(Vector3Int coords, out CommonBlock block);
    }
}