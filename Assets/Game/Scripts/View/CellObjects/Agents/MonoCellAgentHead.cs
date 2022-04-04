using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.Repos;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Agents
{
    public class MonoCellAgentHead : AbstractMonoCellObject
    {
        [SerializeField] private MonoCellAgentLegs _monoCellAgentLegs;
        [SerializeField] private bool _isExternallyModifiable;
        [SerializeField] private Material _selectedMaterial;

        private MeshRenderer _mesh;
        private Material[] _meshMaterials;
    
        public override AbstractChildCellObject Init(IdRepoWithFactory<AbstractChildCellObject> cellObjectRepo, 
            Func<Vector3Int, Vector3> coordsToPositionConvert)
        {
            if (_isInit) return null;
            _isInit = true;
            _coordsToPositionConvert = coordsToPositionConvert;
            return MakeCellObject(cellObjectRepo);
        }
        
        private void OnEnable()
        {
            _mesh = GetComponent<MeshRenderer>();
            _meshMaterials = _mesh.materials;
        }

        public override bool IsExternallyModifiable => _isExternallyModifiable;
        public override CellObjectType CellObjectType => CellObjectType.Agent;

        public override void CommitAction(object sender, PerformanceParams performanceParams)
        {
            if (!(performanceParams.RawActionType is CellBlockViewAction cellBlockViewAction)) return;
            switch (cellBlockViewAction)
            {
                case CellBlockViewAction.Select:
                    var materialsWithSelect = _meshMaterials.ToList();
                    materialsWithSelect.Add(_selectedMaterial);
                    _mesh.materials = materialsWithSelect.ToArray();
                    return;
                case CellBlockViewAction.Unselect:
                    _mesh.materials = _meshMaterials;
                    return;
                case CellBlockViewAction.Dispose:
                    Destroy(gameObject);
                    return;
                case CellBlockViewAction.Error:
                    _mesh.material.color = Color.red;
                    return;
                case CellBlockViewAction.ApplyGravity:
                    var newCoords = performanceParams.FlexibleData.Vector3IntParams.GetParam("NewCoords");
                    if (!newCoords.HasValue)
                        throw new ArgumentException("Performance params doesn't contains new coords");
                    _mesh.material.color = Color.green;
                    var newPosition = _coordsToPositionConvert(newCoords.Value);
                    transform.position = newPosition;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellBlockViewAction), cellBlockViewAction, null);
            }
        }

        private AbstractChildCellObject MakeCellObject(IdRepoWithFactory<AbstractChildCellObject> cellObjectRepo)
        {
            var (cellAgentHead, cellAgentLegs) = cellObjectRepo
                .MakeAndAdd((id1, id2) => (
                    new CellAgentHead(id1, id2, CommitAction, _isExternallyModifiable),
                    new CellAgentLegs(id2, id1, CommitAction, _isExternallyModifiable)));
            
            Id = cellAgentHead.Id;
            _monoCellAgentLegs.CellObject = cellAgentLegs;
            return new CellAgentHead(Id, _monoCellAgentLegs.Id, CommitAction, _isExternallyModifiable);
        }
    }
}