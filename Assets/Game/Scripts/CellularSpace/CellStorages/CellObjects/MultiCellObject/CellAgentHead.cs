using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.MultiCellObject
{
    public class CellAgentHead : AbstractCellObjectMainPart
    {
        public CellAgentHead(int id, int[] partsIds,
            Action<object, PerformanceParams> commitReaction,
            bool isExternallyModifiable) : base(id, partsIds, commitReaction, isExternallyModifiable)
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
            ActionPerformanceParams<CellAgentAction> actionResult;

            switch (cellAgentAction)
            {
                case CellAgentAction.Select:
                    actionResult = new ActionPerformanceParams<CellAgentAction>(CellAgentAction.Select);
                    _commitReaction?.Invoke(this, actionResult);
                    break;
                case CellAgentAction.Unselect:
                    actionResult = new ActionPerformanceParams<CellAgentAction>(CellAgentAction.Unselect);
                    _commitReaction?.Invoke(this, actionResult);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void CommitBaseAction(object sender, CellObjectBaseAction baseActionType, List<AbstractCellObject> parts)
        {
            ActionPerformanceParams<CellBlockViewAction> viewActionPerformanceParams;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Dispose:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Dispose);
                    foreach (var part in parts)
                    {
                        part?.CommitAction(this, viewActionPerformanceParams);
                    }
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    ParentCell?.Clear();
                    return;
                case CellObjectBaseAction.ApplyGravity:
                    ApplyGravity(parts);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
        }

        private void ApplyGravity(IEnumerable<AbstractCellObject> parts)
        {
            var objectsTargetCells = new List<(AbstractCellObject, Vector3Int)>();
            var allParts = parts.ToList();
            allParts.Add(this);
            foreach (var cellObject in allParts)
            {
                var newCoords = cellObject.Coords - Vector3Int.down;
                if (!cellObject.ParentCellGrid.TryGetCell(newCoords, out var cell))
                    throw new Exception($"CellGrid does not contains cell ont {newCoords}");
                if (cell.IsEmpty || !cell.IsEmpty && allParts.Contains(cell.CellObject))
                    objectsTargetCells.Add((cellObject, cell.Coords));
                else
                    return;
            }

            objectsTargetCells.Sort((otc1, otc2) =>
                otc1.Item1.Coords.y.CompareTo(otc2.Item1.Coords.y));
            foreach (var (cellObject, targetCoords) in objectsTargetCells)
            {
                var viewActionPerformanceParams = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.ApplyGravity);
                viewActionPerformanceParams.FlexibleData.Vector3Params.SetParam("NewCoords", targetCoords);
                ParentCellGrid.SetCell(targetCoords, cellObject.Id);
                
                if (cellObject == this)
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                else
                    cellObject?.CommitAction(this, viewActionPerformanceParams);
            }
        }
    }
}