﻿using System;
 using Game.Scripts.CellularSpace.CellObjects;
 using Game.Scripts.CellularSpace.CellObjects.Enums;
 using Game.Scripts.General.FlexibleDataApi;
 using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization
{
    public abstract class MonoCellObject : MonoBehaviour
    {
        [SerializeField] private AbstractMonoCellObject _monoCellObjectPrefab;

        public Vector3 MainPosition => transform.position;

        public Func<int, AbstractChildCellObject> MakeCellObjectFunc(Grid parentGrid, Func<Vector3Int, Vector3> coordsToPositionConvert)
        {
            var monoCellObject = Instantiate(_monoCellObjectPrefab, parentGrid.transform);
            monoCellObject.transform.position = MainPosition;
            return id =>
            {
                monoCellObject.Init(id, IsModifiable, CellObjectType, coordsToPositionConvert);
                return MakeCellObject(id, monoCellObject.CommitAction, monoCellObject.IsModifiable);
            };
        }
        protected abstract bool IsModifiable { get; }
        protected abstract CellObjectType CellObjectType { get; }
        protected abstract AbstractChildCellObject MakeCellObject(int id, Action<object, PerformanceParam> commitReaction, bool isExternallyModifiable);
    }
}