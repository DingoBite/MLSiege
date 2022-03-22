using UnityEngine;

namespace Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces
{
    public interface IGridCoordsConverter
    {
        void Init(Vector3 cellSize);
        Vector3Int Convert(Vector3 coords);
        Vector3 Convert(Vector3Int coords);
    }
}