using Game.Scripts.CellObjects;
using Game.Scripts.General.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICell : ICoordsLocated
    {
        bool IsEmpty { get; }
        new Vector3Int Coords { get; }
        AbstractCellObject CellObject { get; }
        ICellGrid CellGrid { get; }
        void Clear();
    }
}