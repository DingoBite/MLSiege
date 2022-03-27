using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.General.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public abstract class AbstractCellObject : IActable<FlexibleData>, IActable<CellObjectBaseAction>
    {
        protected readonly Action<object, PerformanceParams> _commitReaction;
        protected ICharacteristics _characteristics;

        protected AbstractCellObject(int id, Action<object, PerformanceParams> commitReaction, bool isExternallyModifiable)
        {
            Id = id;
            _commitReaction = commitReaction;
            IsExternallyModifiable = isExternallyModifiable;
        }
        public int Id { get; }
        public bool IsExternallyModifiable { get; }
        public abstract Vector3Int Coords { get; }
        public abstract ICellGrid ParentCellGrid { get; }
        public CellObjectType CellObjectType => _characteristics.CellObjectType;
        public abstract void CommitAction(object sender, PerformanceParams performanceParams);
    }
}