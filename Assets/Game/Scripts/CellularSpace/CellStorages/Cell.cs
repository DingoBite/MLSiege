using System;
using Game.Scripts.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages
{
    public class Cell : ICellMutable
    {
        private readonly Action<int> _onClearAction;

        public Cell(ICellGrid parentCellGrid, Vector3Int coords, Action<int> onClearAction = null)
        {
            CellGrid = parentCellGrid;
            _onClearAction = onClearAction;
            Coords = coords;
        }

        public Vector3Int Coords { get; }

        public AbstractCellObject CellObject => ChildCellObject;

        public ICellGrid CellGrid { get; }

        public bool IsEmpty => CellObject == null;

        public void SetCellObject(AbstractChildCellObject childCellObject)
        {
            if (childCellObject != null)
            {
                childCellObject.ParentCellMutable = this;
            }
            ChildCellObject = childCellObject;
        }

        public AbstractChildCellObject ChildCellObject { get; private set; }

        public void Clear()
        {
            if (!IsEmpty && _onClearAction != null)
            {
                var id = CellObject.Id;
                _onClearAction.Invoke(id);
            }
            ChildCellObject = null;
        }

        public override string ToString() => $"{Coords} {CellObject?.Id}";
    }
}