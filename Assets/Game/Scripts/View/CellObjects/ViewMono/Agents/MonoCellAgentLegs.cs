using System;
using System.Linq;
using Game.Scripts.CellObjects.Enums.Agent;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.Time.Interfaces;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.ViewMono.Agents
{
    public class MonoCellAgentLegs : AbstractMonoCellObject
    {
        [SerializeField] private Material _selectedMaterial;
        
        private Transform _transform;
        private MeshRenderer _mesh;
        private Material[] _meshMaterials;
        private IOneActUpdateTicker _oneActUpdateTicker;

        private void OnEnable()
        {
            _transform = transform;
            _mesh = GetComponent<MeshRenderer>();
            _meshMaterials = _mesh.materials;
        }

        public override void CommitAction(object sender, PerformanceParam performanceParam)
        {
            if (!(performanceParam.EnumActionType is CellAgentViewAction cellAgentViewAction)) return;
            switch (cellAgentViewAction)
            {
                case CellAgentViewAction.Select:
                    var materialsWithSelect = _meshMaterials.ToList();
                    materialsWithSelect.Add(_selectedMaterial);
                    _mesh.materials = materialsWithSelect.ToArray();
                    break;
                case CellAgentViewAction.Unselect:
                    _mesh.materials = _meshMaterials;
                    break;
                case CellAgentViewAction.Dispose:
                    Destroy(gameObject);
                    break;
                case CellAgentViewAction.Error:
                    _mesh.material.color = Color.red;
                    break;
                case CellAgentViewAction.MoveToCoords:
                    MoveTo(performanceParam.Vector3IntParam);
                    break;
                case CellAgentViewAction.StepMove:
                    MoveTo(performanceParam.Vector3IntParam);
                    break;
                case CellAgentViewAction.JumpMove:
                    MoveTo(performanceParam.Vector3IntParam);
                    break;
                case CellAgentViewAction.ApplyGravity:
                    MoveTo(performanceParam.Vector3IntParam);
                    break;
                case CellAgentViewAction.Hit:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellAgentViewAction), cellAgentViewAction, null);
            }
        }
        
        private void MoveTo(Vector3Int? coords)
        {
            if (coords == null)
                throw new ArgumentException("Performance params doesn't contains new coords");
            _transform.position = _coordsToPositionConvert(coords.Value);
        }
    }
}