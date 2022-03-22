using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
{
    public class MonoCellBlock : AbstractMonoCellObject
    {
        public override AbstractCellObject GetCellObject() => new CellBlock();

        protected override CellObjectType GetCellObjectType() => CellObjectType.Block;

        public override void CommitAction(FlexibleData cellObjectFuncResult)
        {
            var color = cellObjectFuncResult?.GetVector4Param("Color");
            if (color == null) return;
            GetComponent<MeshRenderer>().material.color = color.Value;
        }
    }
}