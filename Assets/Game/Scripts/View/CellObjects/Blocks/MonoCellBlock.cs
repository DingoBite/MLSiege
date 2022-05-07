using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellObjects.Enums.Block;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.Time.Interfaces;
using Game.Scripts.View.CellObjects.Agents;
using UnityEngine;
using Zenject;

namespace Game.Scripts.View.CellObjects.Blocks
{
    public class MonoCellBlock : AbstractMonoCellObject
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
                case CellBlockViewAction.GetHit:
                    Debug.Log(performanceParam.IntParam);
                    break;
                case CellBlockViewAction.Destroy:
                    Destroy(gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellBlockViewAction), cellBlockViewAction, null);
            }
        }
        
        private void MoveTo(Vector3Int? coords)
        {
            if (coords == null)
                throw new ArgumentException("Performance params doesn't contains new coords");
            MakeInMainThread(() => _transform.position = _coordsToPositionConvert(coords.Value));
        }
    }
}