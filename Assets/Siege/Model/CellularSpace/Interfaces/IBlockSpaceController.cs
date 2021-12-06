using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.View.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockSpaceController
    {
        public bool InsertBlock(Vector3Int coords, BlockInfo blockInfo, out int id);
        public bool InsertBlock(Vector3Int coords, BlockInfo blockInfo);
        public bool InsertBlock(MonoBlock block, out int id);
        public bool InsertBlock(MonoBlock block);
        public void SwapBlock(int id1, int id2);
        public void SwapBlock(Vector3Int cords1, Vector3Int cords2);
        public void DeleteBlock(Vector3Int coords);
        public void DeleteBlock(int id);
        public void Clear();
    }
}