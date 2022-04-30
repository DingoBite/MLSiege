using System;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects
{
    public abstract class AbstractChildCellObject : AbstractCellObject
    {
        public ICellMutable ParentCell
        {
            get => _parentCell;
            set => _parentCell = value ?? throw new ArgumentNullException();
        }
        private ICellMutable _parentCell;
        
        protected AbstractChildCellObject(int id, Action<object, PerformanceParam> commitReaction, bool isModifiable)
            : base(id, commitReaction, isModifiable)
        {
        }

        public override Vector3Int Coords => ParentCell.Coords;
        public override ICellGrid ParentCellGrid => ParentCell.CellGrid;
    }
}