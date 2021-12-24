using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.CellularSpace.Repositories.Interfaces
{
    public interface IFrameSpaceDataController<in TInfo, in TMono> where TMono : CellularSpaceMonoObject<TInfo>
    {
        public bool Insert(Vector3Int coords, TInfo info, out int id);
        public bool Insert(Vector3Int coords, TInfo info);
        public bool Insert(TMono mono, out int id);
        public bool Insert(TMono mono);
        public void Delete(Vector3Int coords);
        public void Delete(int id);
        public void Clear();
    }
}