using System.Collections.Generic;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockSpace
    {
        public IEnumerable<AbstractBlock> GetCustomers();
        public bool TryGetBlock(int id, out AbstractBlock block);
        public bool TryGetBlock(Vector3Int coords, out AbstractBlock block);
        public int InsertBlock(Vector3Int coords, AbstractBlock block);
        public int InsertBlock(MonoBlock block);
        public void SwapBlock(int id1, int id2);
        public void SwapBlock(Vector3Int cords1, Vector3Int cords2);
        public void DeleteBlock(Vector3Int coords);
        public void Clear();
    }
}