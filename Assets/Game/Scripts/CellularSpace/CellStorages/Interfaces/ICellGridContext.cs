using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellObjects;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellGridContext
    {
        bool TryGetCellObject(int id, out AbstractCellObject cellObject);
        bool TryGetCell(Vector3Int coords, out ICell cell);
        bool IsEmpty(Vector3Int coords);
        bool IsInGrid(Vector3Int coords);
        AbstractCellObject GetCellObject(int id);
        IEnumerable<ICell> GetCells();
    }
}