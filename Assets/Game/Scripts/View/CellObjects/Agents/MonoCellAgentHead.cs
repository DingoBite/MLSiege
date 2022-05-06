using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellObjects.Enums.Agent;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Agents
{
    public class MonoCellAgentHead : AbstractMonoCellObject
    {
        [SerializeField] private Material _selectedMaterial;

        private MeshRenderer _mesh;
        private Material[] _meshMaterials;
        
        private void OnEnable()
        {
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
                    Debug.Log("Hit block");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellAgentViewAction), cellAgentViewAction, null);
            }
        }

        private void MoveTo(Vector3Int? coords)
        {
            if (coords == null)
                throw new ArgumentException("Performance params doesn't contains new coords");
            transform.position = _coordsToPositionConvert(coords.Value);
        }
    }
}