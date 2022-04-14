using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellGridEditor
    {
        bool TrySetCellObjectTo(Vector3Int coords, int cellObjectId);
        bool TrySwapCellObject(Vector3Int coords1, Vector3Int coords2);
        bool TryMoveCellObjectTo(Vector3Int coords, int cellObjectId);
        void SetCellObjectToCoords(Vector3Int coords, int cellObjectId);
    }
}