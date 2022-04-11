using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.Repos;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Agents
{
    public class MonoCellAgentLegs : AbstractMonoCellObject
    {
        [SerializeField] private bool _isExternallyModifiable;
        [SerializeField] private Material _selectedMaterial;

        private MeshRenderer _mesh;
        private Material[] _meshMaterials;

        private void OnEnable()
        {
            _mesh = GetComponent<MeshRenderer>();
            _meshMaterials = _mesh.materials;
        }

        public override bool IsExternallyModifiable => _isExternallyModifiable;
        public override CellObjectType CellObjectType => CellObjectType.Agent;
        
        public override void CommitAction(object sender, PerformanceParams performanceParams)
        {
            if (!(performanceParams.RawActionType is CellAgentViewAction cellAgentViewAction)) return;
            switch (cellAgentViewAction)
            {
                case CellAgentViewAction.Select:
                    var materialsWithSelect = _meshMaterials.ToList();
                    materialsWithSelect.Add(_selectedMaterial);
                    _mesh.materials = materialsWithSelect.ToArray();
                    return;
                case CellAgentViewAction.Unselect:
                    _mesh.materials = _meshMaterials;
                    return;
                case CellAgentViewAction.Dispose:
                    Destroy(gameObject);
                    return;
                case CellAgentViewAction.Error:
                    _mesh.material.color = Color.red;
                    return;
                case CellAgentViewAction.ApplyGravity:
                    var newCoords = performanceParams.FlexibleData.Vector3IntParams.GetParam("NewCoords");
                    if (!newCoords.HasValue)
                        throw new ArgumentException("Performance params doesn't contains new coords");
                    var newPosition = _coordsToPositionConvert(newCoords.Value);
                    transform.position = newPosition;
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellAgentViewAction), cellAgentViewAction, null);
            }
        }
    }
}