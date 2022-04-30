using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellObjects.Enums.Block;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Blocks
{
    public class MonoCellBlock : AbstractMonoCellObject
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

        public override bool IsModifiable => _isExternallyModifiable;
        public override CellObjectType CellObjectType => CellObjectType.Block;

        public override void CommitAction(object sender, PerformanceParam performanceParam)
        {
            if (!(performanceParam.EnumActionType is CellBlockViewAction cellBlockViewAction)) return;
            switch (cellBlockViewAction)
            {
                case CellBlockViewAction.Select:
                    var materialsWithSelect = _meshMaterials.ToList();
                    materialsWithSelect.Add(_selectedMaterial);
                    _mesh.materials = materialsWithSelect.ToArray();
                    break;
                case CellBlockViewAction.Unselect:
                    _mesh.materials = _meshMaterials;
                    break;
                case CellBlockViewAction.Dispose:
                    Destroy(gameObject);
                    break;
                case CellBlockViewAction.Error:
                    _mesh.material.color = Color.red;
                    break;
                case CellBlockViewAction.StepMove:
                    MoveTo(performanceParam.Vector3IntParam);
                    break;
                case CellBlockViewAction.MoveToCoords:
                    MoveTo(performanceParam.Vector3IntParam);
                    break;
                case CellBlockViewAction.ApplyGravity:
                    MoveTo(performanceParam.Vector3IntParam);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellBlockViewAction), cellBlockViewAction, null);
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