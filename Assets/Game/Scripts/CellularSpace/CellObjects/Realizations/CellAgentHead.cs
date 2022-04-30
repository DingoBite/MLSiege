﻿using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellObjects.ComplexCellObject;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellObjects.Enums.Agent;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects.Realizations
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
                    return ApplyGravity(parts);
                case CellObjectBaseAction.StepMove:
                    return StepMove(parts, performanceParam.Vector3IntParam);
                case CellObjectBaseAction.MoveToCoords:
                    return MoveToCoords(parts, performanceParam.Vector3IntParam);
                default:
                    _commitReaction.Invoke(this, CellAgentViewActions.Error);
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
            return true;
        }

        private bool MoveToCoords(IReadOnlyList<AbstractCellObject> parts, Vector3Int? coords)
        {
            if (coords == null)
                throw new ArgumentException("Performance params doesn't contains coords");
            return ComplexMove(parts, coords.Value, CellAgentViewAction.MoveToCoords);
        }

        private bool StepMove(IReadOnlyList<AbstractCellObject> parts, Vector3Int? direction)
        {
            if (direction == null)
                throw new ArgumentException("Performance params doesn't contains coords");
            var targetCoords = Coords + direction.Value;
            return  ComplexMove(parts, targetCoords, CellAgentViewAction.StepMove) ||
                    ComplexMove(parts, targetCoords + Vector3Int.up, CellAgentViewAction.JumpMove);
        }

        private bool ApplyGravity(IReadOnlyList<AbstractCellObject> parts)
        {
            var targetCoords = Coords + Vector3Int.down;
            return ComplexMove(parts, targetCoords, CellAgentViewAction.ApplyGravity);
        }

        private bool ComplexMove(IReadOnlyList<AbstractCellObject> parts, Vector3Int coords,
            CellAgentViewAction cellAgentViewAction)
        {
            var legs = parts[0];
            var newHeadCoords = coords;
            var newLegsCoords = coords + Vector3Int.down;

            if (!ParentCellGrid.IsInGrid(newHeadCoords) || !ParentCellGrid.IsInGrid(newLegsCoords)) return false;
            
            if (newHeadCoords == legs.Coords)
            {
                if (!ParentCellGrid.TryMoveCellObjectTo(newLegsCoords, legs.Id)) return false;
                ParentCellGrid.TryMoveCellObjectTo(newHeadCoords, Id);
            }
            else if (newLegsCoords == Coords)
            {
                if (!ParentCellGrid.TryMoveCellObjectTo(newHeadCoords, Id)) return false;
                ParentCellGrid.TryMoveCellObjectTo(newLegsCoords, legs.Id);
            }
            else if (ParentCellGrid.IsEmpty(newHeadCoords) && ParentCellGrid.IsEmpty(newLegsCoords))
            {
                ParentCellGrid.TryMoveCellObjectTo(newHeadCoords, Id);
                ParentCellGrid.TryMoveCellObjectTo(newLegsCoords, legs.Id);
            }
            else return false;

            var legsActionType = cellAgentViewAction switch
            {
                CellAgentViewAction.StepMove => CellObjectBaseAction.StepMove,
                CellAgentViewAction.JumpMove => CellObjectBaseAction.StepMove,
                CellAgentViewAction.MoveToCoords => CellObjectBaseAction.MoveToCoords,
                CellAgentViewAction.ApplyGravity => CellObjectBaseAction.ApplyGravity,
                _ => throw new ArgumentOutOfRangeException(nameof(cellAgentViewAction), cellAgentViewAction, null)
            };

            var legsViewParams = new ActPerformanceParam<CellObjectBaseAction>(legsActionType,
                vector3IntParam: newLegsCoords);
            var headViewParams = new ActPerformanceParam<CellAgentViewAction>(cellAgentViewAction,
                vector3IntParam: newHeadCoords);
            _commitReaction.Invoke(this, headViewParams);
            return legs.CommitAction(this, legsViewParams);;
        }
    }
}