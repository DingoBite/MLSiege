using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.Interfaces;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
{
    public abstract class AbstractMonoCellObject: MonoBehaviour, IActable<PerformanceParam>
    {
        public int Id { get; private set; }
        protected Func<Vector3Int, Vector3> _coordsToPositionConvert;
        private bool _isInit;

        public void Init(int id, Func<Vector3Int, Vector3> coordsToPositionConvert)
        {
            if (_isInit) 
                throw new Exception($"Try to reinit AbstractMonoCellObject {this}");
            _isInit = true;
            Id = id;
            _coordsToPositionConvert = coordsToPositionConvert;
        }

        public abstract bool IsModifiable { get; }
        public abstract CellObjectType CellObjectType { get; }
        public abstract void CommitAction(object sender, PerformanceParam performanceParam);
    }
}
