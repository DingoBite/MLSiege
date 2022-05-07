using System;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.Interfaces;
using Game.Scripts.Time.Interfaces;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
{
    public abstract class AbstractMonoCellObject: MonoBehaviour, IActable<PerformanceParam>, IIdentifiable
    {
        protected Func<Vector3Int, Vector3> _coordsToPositionConvert;
        private IOneActUpdateTicker _mainThreadUpdateTicker;
        private bool _isInit;

        public void Init(int id, bool isModifiable, CellObjectType cellObjectType, Func<Vector3Int, Vector3> coordsToPositionConvert,
            IOneActUpdateTicker mainThreadUpdateTicker)
        {
            if (_isInit) 
                throw new Exception($"Try to reinit AbstractMonoCellObject {this}");
            IsModifiable = isModifiable;
            CellObjectType = cellObjectType;
            _isInit = true;
            Id = id;
            _coordsToPositionConvert = coordsToPositionConvert;
            _mainThreadUpdateTicker = mainThreadUpdateTicker;
        }
        
        public int Id { get; private set; }
        public bool IsModifiable { get; private set; }
        public CellObjectType CellObjectType { get; private set; }
        public abstract void CommitAction(object sender, PerformanceParam performanceParam);

        protected void MakeInMainThread(Action action)
        {
            _mainThreadUpdateTicker.AddAction(action);
        }
    }
}
