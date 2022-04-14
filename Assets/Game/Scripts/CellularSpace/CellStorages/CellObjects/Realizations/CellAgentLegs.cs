using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums.Agent;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums.Block;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations.ComplexCellObject;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.StaticUtils;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations
{
    public class CellAgentLegs : AbstractCellObjectPart
    {
        public CellAgentLegs(int id, int mainPartId, 
            Action<object, PerformanceParam> commitReaction, bool isModifiable) 
            : base(id, mainPartId, commitReaction, isModifiable)
        {
        }

        protected override bool OnCommit(object sender, PerformanceParam performanceParam)
        {
            if (!(performanceParam.EnumActionType is CellAgentAction cellAgentAction))
            {
                if (performanceParam.EnumActionType is CellObjectBaseAction cellAgentBaseAction)
                    return CommitBaseAction(sender, performanceParam, cellAgentBaseAction);
                return false;
            }

            switch (cellAgentAction)
            {
                default:
                    _commitReaction?.Invoke(this, CellAgentViewActions.Error);
                    throw new ArgumentOutOfRangeException(nameof(cellAgentAction), cellAgentAction, null);
            }
        }

        protected override bool CommitBaseAction(object sender, PerformanceParam performanceParam, CellObjectBaseAction baseActionType)
        {
            switch (baseActionType)
            {
                case CellObjectBaseAction.Select:
                    _commitReaction?.Invoke(this, CellAgentViewActions.Select);
                    break;
                case CellObjectBaseAction.Unselect:
                    _commitReaction?.Invoke(this, CellAgentViewActions.Unselect);
                    break;
                case CellObjectBaseAction.Dispose:
                    _commitReaction?.Invoke(this, CellAgentViewActions.Dispose);
                    ParentCell?.Clear();
                    break;
                case CellObjectBaseAction.ApplyGravity:
                    if (!performanceParam.IsHaveVector3IntParam()) return false;
                    var newCoords = performanceParam.Vector3IntParam.Value;
                    if (!ParentCellGrid.TryMoveCellObjectTo(newCoords, Id)) return false;
                    var gravityActionParam = new ActPerformanceParam<CellAgentViewAction>(CellAgentViewAction.MoveToCoords,
                        vector3IntParam: newCoords);
                    _commitReaction?.Invoke(this, gravityActionParam);
                    break;
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
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
            return true;
        }
        
        private bool MoveTo(Vector3Int coords)
        {
            if (coords == Coords) return false;
            if (!ParentCellGrid.TryMoveCellObjectTo(coords, Id)) return false;
            
            var viewActionPerformanceParams = new ActPerformanceParam<CellAgentViewAction>(CellAgentViewAction.MoveToCoords,
                vector3IntParam: coords);
            _commitReaction?.Invoke(this, viewActionPerformanceParams);
            return true;
        }
        
        private bool MoveOnDirection(CellObjectBaseAction direction)
        {
            var targetCoordsNullable = Coords + VectorIntDirection.VectorFromDirection(direction);
            if (!targetCoordsNullable.HasValue) return false;
            return MoveTo(targetCoordsNullable.Value);
        }
    }
}