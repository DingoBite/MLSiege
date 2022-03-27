using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellGridEditor
    {
        bool TrySetCellObjectTo(Vector3Int coords, int cellObjectId);
        void SetCell(Vector3Int coords, int cellObjectId);
    }
}