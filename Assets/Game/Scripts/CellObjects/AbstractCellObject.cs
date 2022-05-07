using System;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects
{
    public abstract class AbstractCellObject : IActableReturnsBool<PerformanceParam>, IIdentifiable
    {
        protected readonly Action<object, PerformanceParam> _commitReaction;

        protected AbstractCellObject(int id, Action<object, PerformanceParam> commitReaction, bool isModifiable)
        {
            Id = id;
            _commitReaction = commitReaction;
            IsModifiable = isModifiable;
        }

        public int Id { get; }
        public bool IsModifiable { get; }
        public abstract ICharacteristics Characteristics { get; }
        public abstract Vector3Int Coords { get; }
        public abstract ICell ParentCell { get; }
        public abstract ICellGrid ParentCellGrid { get; }
        public virtual int? EvaluateCell(ICell cell) => null;
        public CellObjectType CellObjectType => Characteristics.CellObjectType;
        public abstract bool CommitAction(object sender, PerformanceParam performanceParam);
    }
}