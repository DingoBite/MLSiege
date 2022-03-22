using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.StaticUtils.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public class CellBlock : AbstractCellObject
    {
        private readonly FlexibleData _actionCommitCashedResult = new FlexibleData();
        
        public override FlexibleData CommitAction(FlexibleData flexibleData)
        {
            var actionType = flexibleData.GetIntParam("ActionType");
            if (actionType == null)
            {
                _actionCommitCashedResult.SetVector4Param("Color", Color.red);
                return _actionCommitCashedResult;
            }
            else
            {
                var actionTypeEnum = EnumInfo<CellBlockAction, int>.ValueToEnum[actionType.Value];
                switch (actionTypeEnum)
                {
                    case CellBlockAction.Kek:
                        _actionCommitCashedResult.SetVector4Param("Color", Color.black);
                        return _actionCommitCashedResult;
                    case CellBlockAction.Lol:
                        _actionCommitCashedResult.SetVector4Param("Color", Color.blue);
                        return _actionCommitCashedResult;
                    case CellBlockAction.Lel:
                        _actionCommitCashedResult.SetVector4Param("Color", Color.yellow);
                        return _actionCommitCashedResult;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}