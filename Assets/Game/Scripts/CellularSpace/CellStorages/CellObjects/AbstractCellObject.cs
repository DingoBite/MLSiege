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
        public int Id { get; }
        public bool IsIndependent { get; }
        protected readonly Action<object, PerformanceParams> _commitReaction;
        protected ICharacteristics _characteristics;

        protected AbstractCellObject(int id, Action<object, PerformanceParams> commitReaction, bool isIndependent = true)
        {
            Id = id;
            _commitReaction = commitReaction;
            IsIndependent = isIndependent;
        }
        
        public abstract Vector3Int Coords { get; }
        public CellObjectType CellObjectType => _characteristics.CellObjectType;
        public abstract void CommitAction(object sender, PerformanceParams performanceParams);
    }
}