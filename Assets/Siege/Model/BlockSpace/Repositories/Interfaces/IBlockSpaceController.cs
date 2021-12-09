using Assets.Siege.View.Blocks.Abstracts;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Repositories.Interfaces
{
    public interface IBlockSpaceController<in TInfo, in TMono> where TMono : BlockSpaceMonoObject<TInfo>
    {
        public bool InsertBlock(Vector3Int coords, TInfo blockInfo, out int id);
        public bool InsertBlock(Vector3Int coords, TInfo blockInfo);
        public bool InsertBlock(TMono block, out int id);
        public bool InsertBlock(TMono block);
        public void SwapBlock(int id1, int id2);
        public void SwapBlock(Vector3Int cords1, Vector3Int cords2);
        public void MoveBlock(int id, Vector3Int newCoords);
        public void MoveBlock(Vector3Int blockCoords, Vector3Int newCoords);
        public void DeleteBlock(Vector3Int coords);
        public void DeleteBlock(int id);
        public void Clear();
    }
}