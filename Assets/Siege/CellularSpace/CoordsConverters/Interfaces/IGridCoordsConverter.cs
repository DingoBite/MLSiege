using UnityEngine;

namespace Assets.Siege.CellularSpace.CoordsConverters.Interfaces
{
    public interface IGridCoordsConverter
    {
        public void Init(Vector3 cellSize);
        public Vector3Int Convert(Vector3 coords);
        public Vector3 Convert(Vector3Int coords);
    }
}