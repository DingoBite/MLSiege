using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.MultiCellObject;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations
{
    public class CellAgentHead : AbstractCellObjectMainPart
    {
        public CellAgentHead(int id, int legsId, 
            Action<object, PerformanceParams> commitReaction, bool isExternallyModifiable) 
            : base(id, new []{legsId}, commitReaction, isExternallyModifiable)
        {
        }

        public CellAgentHead(int id, Action<object, PerformanceParams> commitReaction,
            bool isExternallyModifiable) : base(id, commitReaction, isExternallyModifiable)
        {
        }
        
        protected override void OnCommit(object sender, PerformanceParams performanceParams, List<AbstractCellObject> parts)
        {
            if (!(performanceParams.RawActionType is CellAgentAction cellAgentAction))
            {
                if (performanceParams.RawActionType is CellObjectBaseAction cellAgentBaseAction)
                    CommitBaseAction(sender, cellAgentBaseAction, parts);
                return;
            }
            ActionPerformanceParams<CellAgentViewAction> mainPartViewPerformanceParams;
            ActionPerformanceParams<CellAgentAction> partPerformanceParams;

            switch (cellAgentAction)
            {
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnCommitFromPart(AbstractCellObject part, PerformanceParams performanceParams, List<AbstractCellObject> parts)
        {
            OnCommit(this, performanceParams, parts);
        }

        protected override void CommitBaseAction(object sender, CellObjectBaseAction baseActionType, List<AbstractCellObject> parts)
        {
            ActionPerformanceParams<CellAgentViewAction> viewActionPerformanceParams;
            ActionPerformanceParams<CellObjectBaseAction> partActionPerformanceParams;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Select:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.Select);
                    partActionPerformanceParams = new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.Select);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    parts[0].CommitAction(this, partActionPerformanceParams);
                    return;
                case CellObjectBaseAction.Unselect:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.Unselect);
                    partActionPerformanceParams = new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.Unselect);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    parts[0].CommitAction(this, partActionPerformanceParams);
                    return;
                case CellObjectBaseAction.Dispose:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.Dispose);
                    partActionPerformanceParams = new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.Dispose);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    parts[0].CommitAction(this, partActionPerformanceParams);
                    ParentCell?.Clear();
                    return;
                case CellObjectBaseAction.ApplyGravity:
                    ApplyGravity(parts);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
        }
        
        private void ApplyGravity(IReadOnlyList<AbstractCellObject> parts)
        {
            var legs = parts[0];
            var newHeadCoords = legs.Coords;
            var newLegsCoords = legs.Coords + Vector3Int.down;
            if (!ParentCellGrid.TryGetCell(newLegsCoords, out var cell))
                throw new Exception($"CellGrid does not contains cell ont {newLegsCoords}");
            if (!cell.IsEmpty) return;
            
            ParentCellGrid.SetCellObjectToCoords(newLegsCoords, legs.Id);
            ParentCellGrid.SetCellObjectToCoords(newHeadCoords, Id);
            
            var legsActionPerformanceParams = new ActionPerformanceParams<CellObjectBaseAction>(CellObjectBaseAction.ApplyGravity);
            legs.CommitAction(this, legsActionPerformanceParams);
                
            var viewActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.ApplyGravity);
            viewActionPerformanceParams.FlexibleData.Vector3IntParams.SetParam("NewCoords", newHeadCoords);
            _commitReaction?.Invoke(this, viewActionPerformanceParams);
        }
    }
}