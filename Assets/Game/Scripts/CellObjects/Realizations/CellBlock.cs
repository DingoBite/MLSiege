using System;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellObjects.Enums.Block;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects.Realizations
{
    public class CellBlock : AbstractChildCellObject
    {
        public CellBlock(int id, BlockCharacteristic characteristics,
            Action<object, PerformanceParam> commitReaction, bool isModifiable)
            : base(id, commitReaction, isModifiable)
        {
            _characteristic = characteristics;
        }

        private readonly BlockCharacteristic _characteristic;
        public override ICharacteristics Characteristics => _characteristic;
        
        public override bool CommitAction(object sender, PerformanceParam performanceParam)
        {
            if (!(performanceParam.EnumActionType is CellBlockAction cellBlockAction))
            {
                if (performanceParam.EnumActionType is CellObjectBaseAction cellBlockBaseAction)
                    return CommitBaseAction(sender, performanceParam, cellBlockBaseAction);
                return false;
            }
            
            var returnValue = true;
            switch (cellBlockAction)
            { 
                case CellBlockAction.GetHit:
                    var hitValue = performanceParam.IntParam;
                    if (GetHit(hitValue))
                    {
                        var hitPerformanceParam = new ActPerformanceParam<CellBlockViewAction>(CellBlockViewAction.GetHit,
                            intParam: hitValue);
                        _commitReaction.Invoke(this, hitPerformanceParam);
                    }
                    else
                    {
                        Destroy();
                    }
                    break;
                default:
                    _commitReaction.Invoke(this, CellBlockViewActions.Error);
                    throw new ArgumentOutOfRangeException(nameof(cellBlockAction), cellBlockAction, null);
            }
            while (ApplyGravity())
            {}
            return returnValue;
        }

        private bool GetHit(int? hitValue)
        {
            if (hitValue == null)
                throw new ArgumentException("Performance params doesn't contains hit param");
            _characteristic.DurabilityChange(hitValue.Value);
            return _characteristic.IsCorrect;
        }
        
        private bool CommitBaseAction(object sender, PerformanceParam performanceParam, CellObjectBaseAction baseActionType)
        {
            var returnValue = true;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Select:
                    _commitReaction.Invoke(this, CellBlockViewActions.Select);
                    break;
                case CellObjectBaseAction.Unselect:
                    _commitReaction.Invoke(this, CellBlockViewActions.Unselect);
                    break;
                case CellObjectBaseAction.Dispose:
                    Dispose();
                    break;
                case CellObjectBaseAction.ApplyGravity:
                    returnValue = ApplyGravity();
                    break;
                case CellObjectBaseAction.StepMove:
                    returnValue = StepMove(performanceParam.Vector3IntParam);
                    break;
                case CellObjectBaseAction.MoveToCoords:
                    returnValue = MoveTo(performanceParam.Vector3IntParam);
                    break;
                default:
                    _commitReaction.Invoke(this, CellBlockViewActions.Error);
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }

            while (ApplyGravity())
            {}
            return returnValue;
        }

        private void Dispose()
        {
            _commitReaction.Invoke(this, CellBlockViewActions.Dispose);
            ParentCellMutable?.Clear();
        }

        private void Destroy()
        {
            _commitReaction.Invoke(this, CellBlockViewActions.Destroy);
            ParentCellMutable?.Clear();
        }

        private bool MoveTo(Vector3Int? coords, CellBlockViewAction viewAction = CellBlockViewAction.MoveToCoords)
        {
            if (coords == null)
                throw new ArgumentException("Performance params doesn't contains coords");
            
            if (!ParentCellGrid.TryMoveCellObjectTo(coords.Value, Id)) return false;
            
            var viewActionPerformanceParams = new ActPerformanceParam<CellBlockViewAction>(viewAction,
                vector3IntParam: coords.Value);
            _commitReaction.Invoke(this, viewActionPerformanceParams);
            return true;
        }

        private bool StepMove(Vector3Int? direction)
        {
            var targetCoords = Coords + direction;
            return MoveTo(targetCoords, CellBlockViewAction.StepMove);
        }

        private bool ApplyGravity()
        {
            var targetCoords = Coords + Vector3Int.down;
            return MoveTo(targetCoords, CellBlockViewAction.ApplyGravity);
        }
    }
}