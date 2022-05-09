using System.Collections.Generic;
using Game.Scripts.CellObjects;
using Game.Scripts.PathFind;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellGridContext
    {
        bool TryGetCellObject(int id, out AbstractCellObject cellObject);
        bool TryGetCell(Vector3Int coords, out ICell cell);
        IEnumerable<(ICell, StepData)> FindPath(AbstractCellObject startCellObject, Vector3Int coords);
        IEnumerable<(ICell, StepData)> FindPath(AbstractCellObject startCellObject, AbstractCellObject targetCellObject);
        IEnumerable<(ICell, StepData)> FindPath(AbstractCellObject startCellObject, ICell targetCell);
        bool IsEmpty(Vector3Int coords);
        bool IsInGrid(Vector3Int coords);
        AbstractCellObject GetCellObject(int id);
        IEnumerable<ICell> GetCells();
    }
}