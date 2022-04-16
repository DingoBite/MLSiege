using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums.Agent;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations.ComplexCellObject;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.StaticUtils;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations
{
    public class CellAgentHead : AbstractCellObjectMainPart
    {
        public CellAgentHead(int id, int legsId, 
            Action<object, PerformanceParam> commitReaction, bool isModifiable) 
            : base(id, new []{legsId}, commitReaction, isModifiable)
        {
        }

        protected override bool OnCommit(object sender, PerformanceParam performanceParam, List<AbstractCellObject> parts)
        {
            if (!(performanceParam.EnumActionType is CellAgentAction cellAgentAction))
            {
                if (performanceParam.EnumActionType is CellObjectBaseAction cellAgentBaseAction)
                    return CommitBaseAction(sender, performanceParam, cellAgentBaseAction, parts);
                return false;
            }

            switch (cellAgentAction)
            {
                default:
                    _commitReaction.Invoke(this, CellAgentViewActions.Error);
                    throw new ArgumentOutOfRangeException(nameof(cellAgentAction), cellAgentAction, null);
            }
        }

        protected override bool OnCommitFromPart(AbstractCellObject part, PerformanceParam performanceParam, List<AbstractCellObject> parts)
        {
            return OnCommit(this, performanceParam, parts);
        }

        protected override bool CommitBaseAction(object sender, PerformanceParam performanceParam, 
            CellObjectBaseAction baseActionType, List<AbstractCellObject> parts)
        {
            switch (baseActionType)
            {
                case CellObjectBaseAction.Select:
                    if (!parts[0].CommitAction(this, CellObjectBaseActions.Select)) return false;
                    _commitReaction.Invoke(this, CellAgentViewActions.Select);
                    break;
                case CellObjectBaseAction.Unselect:
                    if (!parts[0].CommitAction(this, CellObjectBaseActions.Unselect)) return false;
                    _commitReaction.Invoke(this, CellAgentViewActions.Unselect);
                    break;
                case CellObjectBaseAction.Dispose:
                    if (!parts[0].CommitAction(this, CellObjectBaseActions.Dispose)) return false;
                    _commitReaction.Invoke(this, CellAgentViewActions.Dispose);
                    ParentCell?.Clear();
                    break;
                case CellObjectBaseAction.ApplyGravity:
                    return MoveOnDirectionLegsHead(parts, CellObjectBaseAction.MoveDown);
                case CellObjectBaseAction.MoveUp:
                    return MoveOnDirectionHeadLegs(parts, baseActionType);
                case CellObjectBaseAction.MoveLeft:
                    return MoveOnDirectionWithJump(parts, baseActionType);
                case CellObjectBaseAction.MoveRight:
                    return MoveOnDirectionWithJump(parts, baseActionType);
                case CellObjectBaseAction.MoveForward:
                    return MoveOnDirectionWithJump(parts, baseActionType);
                case CellObjectBaseAction.MoveBack:
                    return MoveOnDirectionWithJump(parts, baseActionType);
                case CellObjectBaseAction.MoveDown:
                    return MoveOnDirectionLegsHead(parts, baseActionType);
                case CellObjectBaseAction.MoveToCoords:
                    if (!performanceParam.IsHaveVector3IntParam())
                        throw new ArgumentException("Performance params doesn't contains coords");
                    return MoveToLegsHead(parts, performanceParam.Vector3IntParam.Value);
                default:
                    _commitReaction.Invoke(this, CellAgentViewActions.Error);
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
            return true;
        }

        private bool MoveToLegsHead(IReadOnlyList<AbstractCellObject> parts, Vector3Int coords)
        {
            var legs = parts[0];
            var newHeadCoords = coords;
            var newLegsCoords = coords + Vector3Int.down;

            var legsApplyGravityParam = new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.MoveToCoords,
                vector3IntParam: newLegsCoords);
            if (!legs.CommitAction(this, legsApplyGravityParam))
                return false;
            
            if (!ParentCellGrid.TryMoveCellObjectTo(coords, Id)) return false;

            var viewActionPerformanceParams = new ActPerformanceParam<CellAgentViewAction>(CellAgentViewAction.MoveToCoords,
                vector3IntParam: newHeadCoords);
            _commitReaction.Invoke(this, viewActionPerformanceParams);
            return true;
        }
        
        private bool MoveToHeadLegs(IReadOnlyList<AbstractCellObject> parts, Vector3Int coords)
        {
            var legs = parts[0];
            var newHeadCoords = coords;
            var newLegsCoords = coords + Vector3Int.down;
            if (!ParentCellGrid.TryMoveCellObjectTo(coords, Id)) return false;

            var legsApplyGravityParam = new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.MoveToCoords,
                vector3IntParam: newLegsCoords);
            if (!legs.CommitAction(this, legsApplyGravityParam))
                return false;
            
            var viewActionPerformanceParams = new ActPerformanceParam<CellAgentViewAction>(CellAgentViewAction.MoveToCoords,
                vector3IntParam: newHeadCoords);
            _commitReaction.Invoke(this, viewActionPerformanceParams);
            return true;
        }
        
        private bool MoveOnDirectionLegsHead(IReadOnlyList<AbstractCellObject> parts, CellObjectBaseAction direction)
        {
            var targetCoordsNullable = Coords + VectorIntDirection.VectorFromDirection(direction);
            if (!targetCoordsNullable.HasValue) return false;
            return MoveToLegsHead(parts, targetCoordsNullable.Value);
        }
        
        private bool MoveOnDirectionHeadLegs(IReadOnlyList<AbstractCellObject> parts, CellObjectBaseAction direction)
        {
            var targetCoordsNullable = Coords + VectorIntDirection.VectorFromDirection(direction);
            if (!targetCoordsNullable.HasValue) return false;
            return MoveToHeadLegs(parts, targetCoordsNullable.Value);
        }
        
        private bool MoveOnDirectionWithJump(IReadOnlyList<AbstractCellObject> parts, CellObjectBaseAction direction)
        {
            var targetCoordsNullable = Coords + VectorIntDirection.VectorFromDirection(direction);
            if (!targetCoordsNullable.HasValue) return false;
            if (MoveToLegsHead(parts, targetCoordsNullable.Value)) return true;
            var jumpCoords = targetCoordsNullable.Value + Vector3Int.up;
            return MoveToLegsHead(parts, jumpCoords);
        }
    }
}