using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.General.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public abstract class AbstractCellObject : IActable<FlexibleData>, IActable<CellObjectBaseAction>, IDisposable
    {
        protected readonly Action<FlexibleData> _commitReaction;
        protected ICharacteristics _characteristics;

        protected AbstractCellObject(Action<FlexibleData> commitReaction)
        {
            _commitReaction = commitReaction;
        }

        public bool IsDisposed { get; private set; }

        protected bool IsReadyToAct()
        {
            if (!IsDisposed) return true;
            Debug.LogError("Try to commit action on destroyed cell object");
            return false;
        }
        
        public CellObjectType CellObjectType => _characteristics.CellObjectType;
        public abstract void CommitAction(FlexibleData flexibleData);
        public abstract void CommitAction(CellObjectBaseAction baseActionType);
        
        public void Dispose()
        {
            OnDispose();
            IsDisposed = true;
        }

        protected abstract void OnDispose();
    }
}