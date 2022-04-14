using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public abstract class AbstractCellObject : IActableReturnsBool<PerformanceParam>
    {
        protected readonly Action<object, PerformanceParam> _commitReaction;
        private ICharacteristics _characteristics;

        protected AbstractCellObject(int id, Action<object, PerformanceParam> commitReaction, bool isModifiable)
        {
            Id = id;
            _commitReaction = commitReaction;
            IsModifiable = isModifiable;
        }
        public int Id { get; }
        public bool IsModifiable { get; }
        public abstract Vector3Int Coords { get; }
        public abstract ICellGrid ParentCellGrid { get; }
        public CellObjectType CellObjectType => _characteristics.CellObjectType;
        public abstract bool CommitAction(object sender, PerformanceParam performanceParam);
    }
}