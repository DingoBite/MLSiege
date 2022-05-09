using System;
using Game.Scripts.CellObjects;
using Game.Scripts.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellObjects.Realizations;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.Time.Interfaces;
using Game.Scripts.View.CellObjects.ViewMono;
using UnityEngine;
using Zenject;

namespace Game.Scripts.View.CellObjects.ViewEditor
{
    public abstract class MonoAgent : MonoBehaviour
    {
        [SerializeField] private AbstractMonoCellObject _monoHeadPrefab;
        [SerializeField] private AbstractMonoCellObject _monoLegsPrefab;

        [SerializeField] private Transform _headTransform;
        [SerializeField] private Transform _legsTransform;
        
        [Inject] private IOneActUpdateTicker _mainThreadUpdateTicker;
        
        public Vector3 MainPosition => _headTransform.position;
        public Vector3 LegsPosition => _legsTransform.position;

        public Func<int, int, (AbstractChildCellObject, AbstractChildCellObject)> MakeCellAgentFunc(Grid parentGrid, 
            Func<Vector3Int, Vector3> coordsToPositionConvert)
        {
            var monoHead = Instantiate(_monoHeadPrefab, parentGrid.transform);
            monoHead.transform.position = MainPosition;
            var monoLegs = Instantiate(_monoLegsPrefab, parentGrid.transform);
            monoLegs.transform.position = LegsPosition;
            
            return (headId, legsId) =>
            {
                monoHead.Init(headId, IsModifiable, CellObjectType.Agent, coordsToPositionConvert, _mainThreadUpdateTicker);
                monoLegs.Init(legsId, IsModifiable, CellObjectType.AgentPart, coordsToPositionConvert, _mainThreadUpdateTicker);
                return MakeCellObject(headId, legsId,
                    monoHead.CommitAction, monoHead.IsModifiable,
                    monoLegs.CommitAction, monoLegs.IsModifiable);
            };
        }
        protected abstract bool IsModifiable { get; }
        protected abstract AgentCharacteristic HeadCharacteristics { get; }
        protected abstract AgentPartCharacteristic LegsCharacteristic { get; }
        private (AbstractChildCellObject, AbstractChildCellObject) MakeCellObject(int headId, int legsId,
            Action<object, PerformanceParam> headCommitReaction, bool headIsExternallyModifiable,
            Action<object, PerformanceParam> legsCommitReaction, bool legsIsExternallyModifiable)
        {
            var head = new CellAgentHead(headId, legsId, HeadCharacteristics,
                headCommitReaction, headIsExternallyModifiable);
            var legs = new CellAgentLegs(legsId, headId, LegsCharacteristic,
                legsCommitReaction, legsIsExternallyModifiable);
            return (head, legs);
        }
    }
}