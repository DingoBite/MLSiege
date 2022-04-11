using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.View.CellObjects.Agents;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Interfaces
{
    public abstract class MonoSoloCellObject : MonoBehaviour, IMonoCellObject
    {
        [SerializeField] private AbstractMonoCellObject _monoCellObjectPrefab;

        public Vector3 MainPosition => transform.position;
        
        public void Start()
        {
            gameObject.SetActive(false);
        }
        
        public Func<int, AbstractChildCellObject> MakeCellObjectFunc(Grid parentGrid, Func<Vector3Int, Vector3> coordsToPositionConvert)
        {
            var monoCellObject = Instantiate(_monoCellObjectPrefab, parentGrid.transform);
            monoCellObject.transform.position = MainPosition;
            return id =>
            {
                monoCellObject.Init(id, coordsToPositionConvert);
                return MakeCellObject(id, monoCellObject.CommitAction, monoCellObject.IsExternallyModifiable);
            };
        }

        protected abstract AbstractChildCellObject MakeCellObject(int id, Action<object, PerformanceParams> commitReaction, bool isExternallyModifiable);
    }
}