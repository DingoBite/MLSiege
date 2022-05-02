using System;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.Interfaces;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
{
    public abstract class AbstractMonoCellObject: MonoBehaviour, IActable<PerformanceParam>, IIdentifiable
    {
        protected Func<Vector3Int, Vector3> _coordsToPositionConvert;
        private bool _isInit;

        public void Init(int id, bool isModifiable, CellObjectType cellObjectType, Func<Vector3Int, Vector3> coordsToPositionConvert)
        {
            if (_isInit) 
                throw new Exception($"Try to reinit AbstractMonoCellObject {this}");
            IsModifiable = isModifiable;
            CellObjectType = cellObjectType;
            _isInit = true;
            Id = id;
            _coordsToPositionConvert = coordsToPositionConvert;
        }
        public int Id { get; private set; }
        public bool IsModifiable { get; private set; }
        public CellObjectType CellObjectType { get; private set; }
        public abstract void CommitAction(object sender, PerformanceParam performanceParam);
    }
}
