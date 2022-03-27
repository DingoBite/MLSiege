using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICell
    {
        bool IsEmpty { get; }
        Vector3Int Coords { get; }
        AbstractCellObject CellObject { get; }
        ICellGrid CellGridContext { get; }
        void Clear();
    }
}