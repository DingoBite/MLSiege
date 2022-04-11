using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations;
using Game.Scripts.General.FlexibleDataApi;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Interfaces
{
    public class MonoAgent : MonoBehaviour, IMonoCellObject
    {
        [SerializeField] private AbstractMonoCellObject _monoHeadPrefab;
        [SerializeField] private AbstractMonoCellObject _monoLegsPrefab;

        [SerializeField] private Transform _headTransform;
        [SerializeField] private Transform _legsTransform;
        
        public Vector3 MainPosition => _headTransform.position;
        public Vector3 LegsPosition => _legsTransform.position;

        public void Start()
        {
            gameObject.SetActive(false);
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
                monoHead.Init(headId, coordsToPositionConvert);
                monoLegs.Init(legsId, coordsToPositionConvert);
                return MakeCellObject(headId, legsId,
                    monoHead.CommitAction, monoHead.IsExternallyModifiable,
                    monoLegs.CommitAction, monoLegs.IsExternallyModifiable);
            };
        }
        
        private (AbstractChildCellObject, AbstractChildCellObject) MakeCellObject(int headId, int legsId,
            Action<object, PerformanceParams> headCommitReaction, bool headIsExternallyModifiable,
            Action<object, PerformanceParams> legsCommitReaction, bool legsIsExternallyModifiable)
        {
            var head = new CellAgentHead(headId, legsId, headCommitReaction, headIsExternallyModifiable);
            var legs = new CellAgentLegs(legsId, headId, legsCommitReaction, legsIsExternallyModifiable);
            return (head, legs);
        }
    }
}