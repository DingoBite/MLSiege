using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums.Block;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.StaticUtils;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations
{
    public class CellBlock : AbstractChildCellObject
    {
        public CellBlock(int id, Action<object, PerformanceParam> commitReaction, bool isModifiable)
            : base(id, commitReaction, isModifiable)
        {
        }

        public override bool CommitAction(object sender, PerformanceParam performanceParam)
        {
            if (!(performanceParam.EnumActionType is CellBlockAction cellBlockAction))
            {
                if (performanceParam.EnumActionType is CellObjectBaseAction cellBlockBaseAction)
                    return CommitBaseAction(sender, performanceParam, cellBlockBaseAction);
                return false;
            }
            switch (cellBlockAction)
            {
                default:
                    _commitReaction?.Invoke(this, CellBlockViewActions.Error);
                    throw new ArgumentOutOfRangeException(nameof(cellBlockAction), cellBlockAction, null);
            }
        }

        private bool CommitBaseAction(object sender, PerformanceParam performanceParam, CellObjectBaseAction baseActionType)
        {
            switch (baseActionType)
            {
                case CellObjectBaseAction.Select:
                    _commitReaction?.Invoke(this, CellBlockViewActions.Select);
                    break;
                case CellObjectBaseAction.Unselect:
                    _commitReaction?.Invoke(this, CellBlockViewActions.Unselect);
                    break;
                case CellObjectBaseAction.Dispose:
                    _commitReaction?.Invoke(this, CellBlockViewActions.Dispose);
                    ParentCell?.Clear();
                    break;
                case CellObjectBaseAction.ApplyGravity:
                    return ApplyGravity();
                case CellObjectBaseAction.MoveUp:
                    return MoveOnDirection(baseActionType);
                case CellObjectBaseAction.MoveLeft:
                    return MoveOnDirection(baseActionType);
                case CellObjectBaseAction.MoveRight:
                    return MoveOnDirection(baseActionType);
                case CellObjectBaseAction.MoveForward:
                    return MoveOnDirection(baseActionType);
                case CellObjectBaseAction.MoveBack:
                    return MoveOnDirection(baseActionType);
                case CellObjectBaseAction.MoveDown:
                    return MoveOnDirection(baseActionType);
                case CellObjectBaseAction.MoveToCoords:
                    if (!performanceParam.IsHaveVector3IntParam())
                        throw new ArgumentException("Performance params doesn't contains coords");
                    return MoveTo(performanceParam.Vector3IntParam.Value);
                default:
                    _commitReaction?.Invoke(this, CellBlockViewActions.Error);
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
            return true;
        }

        private bool MoveTo(Vector3Int coords)
        {
            if (coords == Coords) return false;
            if (!ParentCellGrid.TryMoveCellObjectTo(coords, Id)) return false;
            
            var viewActionPerformanceParams =
                new ActPerformanceParam<CellBlockViewAction>(CellBlockViewAction.MoveToCoords, vector3IntParam: coords);
            _commitReaction?.Invoke(this, viewActionPerformanceParams);
            return true;
        }
        
        private bool MoveOnDirection(CellObjectBaseAction direction)
        {
            var targetCoordsNullable = Coords + VectorIntDirection.VectorFromDirection(direction);
            if (!targetCoordsNullable.HasValue) return false;
            return MoveTo(targetCoordsNullable.Value);
        }
        
        private bool ApplyGravity()
        {
            var targetCoords = Coords + Vector3Int.down;
            return MoveTo(targetCoords);
        }
    }
}