using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public class CellBlock : AbstractChildCellObject
    {
        public CellBlock(int id, Action<object, PerformanceParams> commitReaction, bool isExternallyModifiable)
            : base(id, commitReaction, isExternallyModifiable)
        {
        }

        public override void CommitAction(object sender, PerformanceParams performanceParams)
        {
            if (!(performanceParams.RawActionType is CellBlockAction cellBlockAction))
            {
                if (performanceParams.RawActionType is CellObjectBaseAction cellBlockBaseAction)
                    CommitBaseAction(sender, cellBlockBaseAction);
                return;
            }

            ActionPerformanceParams<CellBlockViewAction> viewActionPerformanceParams;

            switch (cellBlockAction)
            {
                
                default:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Error);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    throw new ArgumentOutOfRangeException(nameof(cellBlockAction), cellBlockAction, null);
            }
        }

        private void CommitBaseAction(object sender, CellObjectBaseAction baseActionType)
        {
            ActionPerformanceParams<CellBlockViewAction> viewActionPerformanceParams;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Select:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Select);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    return;
                case CellObjectBaseAction.Unselect:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Unselect);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    return;
                case CellObjectBaseAction.Dispose:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Dispose);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    ParentCell?.Clear();
                    return;
                case CellObjectBaseAction.ApplyGravity:
                    ApplyGravity();
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
        }
        
        private void ApplyGravity()
        {
            var targetCoords = Coords + Vector3Int.down;
            if (!ParentCellGrid.TryGetCell(targetCoords, out var cell))
                throw new Exception($"CellGrid does not contains cell ont {targetCoords}");
            if (!cell.IsEmpty) return;

            var viewActionPerformanceParams = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.ApplyGravity);
            viewActionPerformanceParams.FlexibleData.Vector3IntParams.SetParam("NewCoords", targetCoords);
            if (!ParentCellGrid.TrySetCellObjectTo(targetCoords, Id)) return;
            
            _commitReaction?.Invoke(this, viewActionPerformanceParams);
        }
    }
}