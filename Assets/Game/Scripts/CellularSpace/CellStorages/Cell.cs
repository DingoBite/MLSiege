using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages
{
    public class Cell : ICell
    {
        private ICellGrid _parentCellGrid;
        public Vector3Int Coords { get; }

        public AbstractCellObject CellObject { get; set; }

        public Cell(ICellGrid parentCellGrid, Vector3Int coords)
        {
            _parentCellGrid = parentCellGrid;
            Coords = coords;
        }

        public bool IsEmpty => CellObject == null;

        public void Clear()
        {
            CellObject = null;
        }
    }
}