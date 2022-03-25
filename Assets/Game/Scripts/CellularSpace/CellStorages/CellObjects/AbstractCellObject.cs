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
        public int Id { get; private set; }
        protected readonly Action _disposeAction;
        protected readonly Action<FlexibleData> _commitReaction;
        protected ICharacteristics _characteristics;

        protected AbstractCellObject(int id, Action<FlexibleData> commitReaction, Action disposeAction)
        {
            Id = id;
            _commitReaction = commitReaction;
            _disposeAction = disposeAction;
        }

        public CellObjectType CellObjectType => _characteristics.CellObjectType;
        public abstract void CommitAction(FlexibleData flexibleData);
        public abstract void CommitAction(CellObjectBaseAction baseActionType);
    }
}