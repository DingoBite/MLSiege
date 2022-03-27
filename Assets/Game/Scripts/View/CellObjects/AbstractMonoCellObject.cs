using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.General.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
{
    public abstract class AbstractMonoCellObject: MonoBehaviour, IActable<FlexibleData>
    {
        public int Id { get; private set; }
        protected Func<Vector3Int, Vector3> _coordsToPositionConvert;
        
        public void Init(int id, Func<Vector3Int, Vector3> coordsToPositionConvert)
        {
            Id = id;
            _coordsToPositionConvert = coordsToPositionConvert;
        }
        
        public abstract bool IsExternallyModifiable { get; } 
        
        protected abstract CellObjectType GetCellObjectType();
        public abstract void CommitAction(object sender, PerformanceParams performanceParams);
    }
}
