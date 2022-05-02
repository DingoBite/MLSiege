using System;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects
{
    public abstract class AbstractChildCellObject : AbstractCellObject
    {
        public ICellMutable ParentCellMutable
        {
            get => _parentCellMutable;
            set => _parentCellMutable = value ?? throw new ArgumentNullException();
        }
        private ICellMutable _parentCellMutable;

        protected AbstractChildCellObject(int id, 
            Action<object, PerformanceParam> commitReaction, bool isModifiable) 
            : base(id, commitReaction, isModifiable)
        {
        }
        
        public override ICell ParentCell => ParentCellMutable;

        public override Vector3Int Coords => ParentCellMutable.Coords;

        public override ICellGrid ParentCellGrid => ParentCellMutable.CellGrid;
    }
}