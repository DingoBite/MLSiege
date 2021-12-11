using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Repositories.Interfaces
{
    public interface IBlockSpaceController<in TInfo, in TMono> where TMono : BlockSpaceMonoObject<TInfo>
    {
        public bool InsertBlock(Vector3Int coords, TInfo info, out int id);
        public bool InsertBlock(Vector3Int coords, TInfo info);
        public bool InsertBlock(TMono mono, out int id);
        public bool InsertBlock(TMono mono);
        public void SwapBlock(int id1, int id2);
        public void SwapBlock(Vector3Int cords1, Vector3Int cords2);
        public void MoveBlock(int id, Vector3Int newCoords);
        public void MoveBlock(Vector3Int coords, Vector3Int newCoords);
        public void DeleteBlock(Vector3Int coords);
        public void DeleteBlock(int id);
        public void Clear();
    }
}