using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
{
    public class MonoCellBlock : AbstractMonoCellObject
    {
        [SerializeField] private Material _selectedMaterial;

        private MeshRenderer _mesh;
        private Material[] _meshMaterials;

        private void OnEnable()
        {
            _mesh = GetComponent<MeshRenderer>();
            _meshMaterials = _mesh.materials;
        }
        
        protected override CellObjectType GetCellObjectType() => CellObjectType.Block;

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
                default:
                    throw new ArgumentOutOfRangeException(nameof(cellBlockViewAction), cellBlockViewAction, null);
            }
        }
    }
}