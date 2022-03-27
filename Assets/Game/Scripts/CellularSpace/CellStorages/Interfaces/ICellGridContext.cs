using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellGridContext
    {
        bool TryGetCellObject(int id, out AbstractCellObject cellObject);
        bool TryGetCell(Vector3Int coords, out ICell cell);
        AbstractCellObject GetCellObject(int id);
        ICell GetCell(Vector3Int coords);
        IEnumerable<ICell> GetCells();
    }
}