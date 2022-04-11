using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.Repos;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
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

        public override bool IsExternallyModifiable => _isExternallyModifiable;
        public override CellObjectType CellObjectType => CellObjectType.Block;

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
    }
}