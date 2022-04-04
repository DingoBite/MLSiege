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
                case CellAgentAction.Select:
                    mainPartViewPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.Select);
                    partPerformanceParams = new ActionPerformanceParams<CellAgentAction>(CellAgentAction.Select);
                    _commitReaction?.Invoke(this, mainPartViewPerformanceParams);
                    parts[0].CommitAction(this, partPerformanceParams);
                    break;
                case CellAgentAction.Unselect:
                    mainPartViewPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.Unselect);
                    partPerformanceParams = new ActionPerformanceParams<CellAgentAction>(CellAgentAction.Select);
                    _commitReaction?.Invoke(this, mainPartViewPerformanceParams);
                    parts[0].CommitAction(this, partPerformanceParams);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void OnCommitFromPart(int partId, PerformanceParams performanceParams, List<AbstractCellObject> parts)
        {
        }

        protected override void CommitBaseAction(object sender, CellObjectBaseAction baseActionType, List<AbstractCellObject> parts)
        {
            ActionPerformanceParams<CellAgentViewAction> viewActionPerformanceParams;
            ActionPerformanceParams<CellObjectBaseAction> partActionPerformanceParams;
            switch (baseActionType)
            {
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
            var newCoords = legs.Coords - Vector3Int.down;
            if (!ParentCellGrid.TryGetCell(newCoords, out var cell))
                throw new Exception($"CellGrid does not contains cell ont {newCoords}");
            if (!cell.IsEmpty) return;
            
            ParentCellGrid.SetCell(newCoords, legs.Id);
            ParentCellGrid.SetCell(legs.Coords, Id);
                
            var legsActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.ApplyGravity);
            legsActionPerformanceParams.FlexibleData.Vector3Params.SetParam("NewCoords", newCoords);
            legs.CommitAction(this, legsActionPerformanceParams);
                
            var viewActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.ApplyGravity);
            viewActionPerformanceParams.FlexibleData.Vector3Params.SetParam("NewCoords", legs.Coords);
            _commitReaction?.Invoke(this, viewActionPerformanceParams);
        }
    }
}