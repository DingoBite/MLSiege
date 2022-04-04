using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.General.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.Repos;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
{
    public abstract class AbstractMonoCellObject: MonoBehaviour, IActable<FlexibleData>
    {
        public int Id { get; protected set; }
        protected Func<Vector3Int, Vector3> _coordsToPositionConvert;
        protected bool _isInit;

        public abstract AbstractChildCellObject Init(IdRepoWithFactory<AbstractChildCellObject> cellObjectRepo,
            Func<Vector3Int, Vector3> coordsToPositionConvert);

        public abstract bool IsExternallyModifiable { get; } 
        
        public abstract CellObjectType CellObjectType { get; }
        public abstract void CommitAction(object sender, PerformanceParams performanceParams);
    }
}
