using System;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public abstract class AbstractChildCellObject : AbstractCellObject
    {
        public ICell ParentCell
        {
            get => _parentCell;
            set => _parentCell = value ?? throw new ArgumentNullException();
        }
        private ICell _parentCell;
        
        protected AbstractChildCellObject(int id, Action<object, PerformanceParams> commitReaction, bool isExternallyModifiable)
            : base(id, commitReaction, isExternallyModifiable)
        {
        }

        public override Vector3Int Coords => ParentCell.Coords;
        public override ICellGrid ParentCellGrid => ParentCell.CellGrid;
    }
}