using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellGridEditor
    {
        bool TrySetCellObjectTo(Vector3Int coords, int cellObjectId);
        void SetCellObjectToCoords(Vector3Int coords, int cellObjectId);
        void SetCellObjectToIndex(Vector3Int coords, int cellObjectId);
    }
}