using System.Collections.Generic;
using Assets.Siege.Model.CellularSpace.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IPackBlockSpaceContext
    {
        public IEnumerable<PackBlock> GetPackBlocks();
        public bool GetPackBlock(int id, out PackBlock block);
        public bool GetPackBlock(Vector3Int coords, out PackBlock block);
    }
}