using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICell
    {
        bool IsEmpty { get; }
        Vector3Int Coords { get; }
        AbstractCellObject CellObject { get; }
        ICellGridContext CellGridContext { get; }
        void SetCellObject(AbstractChildCellObject childCellObject);
        void Clear();
    }
}