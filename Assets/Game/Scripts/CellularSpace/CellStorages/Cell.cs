using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages
{
    public class Cell : ICellMutable
    {
        private readonly Action<int> _onClearAction;

        public Cell(ICellGrid parentCellGrid, Vector3Int coords, Action<int> onClearAction = null)
        {
            CellGridContext = parentCellGrid;
            _onClearAction = onClearAction;
            Coords = coords;
        }

        public Vector3Int Coords { get; }

        public AbstractCellObject CellObject { get; private set; }

        public ICellGrid CellGridContext { get; }

        public bool IsEmpty => CellObject == null;

        public void SetCellObject(AbstractChildCellObject childCellObject)
        {
            childCellObject.ParentCell = this;
            CellObject = childCellObject;
        }

        public void Clear()
        {
            if (!IsEmpty && _onClearAction != null)
            {
                var id = CellObject.Id;
                _onClearAction.Invoke(id);
            }
            CellObject = null;
        }
    }
}