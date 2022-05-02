﻿using System;
 using Game.Scripts.CellularSpace.CellObjects;
 using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
 using Game.Scripts.CellularSpace.CellObjects.Enums;
 using Game.Scripts.CellularSpace.CellObjects.Realizations;
 using Game.Scripts.General.FlexibleDataApi;
 using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization
{
    public abstract class MonoAgent : MonoBehaviour
    {
        [SerializeField] private AbstractMonoCellObject _monoHeadPrefab;
        [SerializeField] private AbstractMonoCellObject _monoLegsPrefab;

        [SerializeField] private Transform _headTransform;
        [SerializeField] private Transform _legsTransform;
        
        public Vector3 MainPosition => _headTransform.position;
        public Vector3 LegsPosition => _legsTransform.position;
        
        protected virtual void OnStart() {}
        
        public void Start()
        {
            gameObject.SetActive(false);
            OnStart();
        }

        public Func<int, int, (AbstractChildCellObject, AbstractChildCellObject)> MakeCellAgentFunc(Grid parentGrid, 
            Func<Vector3Int, Vector3> coordsToPositionConvert)
        {
            var monoHead = Instantiate(_monoHeadPrefab, parentGrid.transform);
            monoHead.transform.position = MainPosition;
            var monoLegs = Instantiate(_monoLegsPrefab, parentGrid.transform);
            monoLegs.transform.position = LegsPosition;
            
            return (headId, legsId) =>
            {
                monoHead.Init(headId, IsModifiable, CellObjectType.Agent, coordsToPositionConvert);
                monoLegs.Init(legsId, IsModifiable, CellObjectType.Agent, coordsToPositionConvert);
                return MakeCellObject(headId, legsId,
                    monoHead.CommitAction, monoHead.IsModifiable,
                    monoLegs.CommitAction, monoLegs.IsModifiable);
            };
        }
        protected abstract bool IsModifiable { get; }
        protected abstract AgentCharacteristic HeadCharacteristics { get; }
        private (AbstractChildCellObject, AbstractChildCellObject) MakeCellObject(int headId, int legsId,
            Action<object, PerformanceParam> headCommitReaction, bool headIsExternallyModifiable,
            Action<object, PerformanceParam> legsCommitReaction, bool legsIsExternallyModifiable)
        {
            var head = new CellAgentHead(headId, legsId, HeadCharacteristics,
                headCommitReaction, headIsExternallyModifiable);
            var legs = new CellAgentLegs(legsId, headId, legsCommitReaction, legsIsExternallyModifiable);
            return (head, legs);
        }
    }
}