using System;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellObjects.ComplexCellObject;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellObjects.Enums.Agent;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects.Realizations
{
    public class CellAgentLegs : AbstractCellObjectPart
    {
        public CellAgentLegs(int id, int mainPartId,
            Action<object, PerformanceParam> commitReaction, bool isModifiable)
            : base(id, mainPartId, commitReaction, isModifiable)
        {
        }

        private AbstractCellObject _parent
        {
            get
            {
                if (!ParentCellGrid.TryGetCellObject(_mainPartId, out var head))
                    throw new Exception("Head agent");
                return head;
            }
        }

        public override Vector3Int Coords => ParentCell.Coords;
        public override ICharacteristics Characteristics => _parent.Characteristics;

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
                    _commitReaction.Invoke(this, CellAgentViewActions.Error);
                    throw new ArgumentOutOfRangeException(nameof(cellAgentAction), cellAgentAction, null);
            }
        }

        protected override bool CommitBaseAction(object sender, PerformanceParam performanceParam, CellObjectBaseAction baseActionType)
        {
            switch (baseActionType)
            {
                case CellObjectBaseAction.Select:
                    _commitReaction.Invoke(this, CellAgentViewActions.Select);
                    break;
                case CellObjectBaseAction.Unselect:
                    _commitReaction.Invoke(this, CellAgentViewActions.Unselect);
                    break;
                case CellObjectBaseAction.Dispose:
                    _commitReaction.Invoke(this, CellAgentViewActions.Dispose);
                    ParentCellMutable?.Clear();
                    break;
                case CellObjectBaseAction.ApplyGravity:
                    ApplyGravity(performanceParam.Vector3IntParam);
                    break;
                case CellObjectBaseAction.StepMove:
                    StepMove(performanceParam.Vector3IntParam);
                    break;
                case CellObjectBaseAction.MoveToCoords:
                    MoveTo(performanceParam.Vector3IntParam);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
            return true;
        }

        private void MoveTo(Vector3Int? coords, CellAgentViewAction cellAgentViewAction = CellAgentViewAction.MoveToCoords)
        {
            if (coords == null)
                throw new ArgumentException("Performance params doesn't contains coords");

            var viewActionPerformanceParams = new ActPerformanceParam<CellAgentViewAction>(cellAgentViewAction,
                vector3IntParam: coords);
            _commitReaction.Invoke(this, viewActionPerformanceParams);
        }

        private void StepMove(Vector3Int? coords)
        {
            MoveTo(coords, CellAgentViewAction.StepMove);
        }

        private void ApplyGravity(Vector3Int? coords)
        {
            MoveTo(coords, CellAgentViewAction.ApplyGravity);
        }
    }
}