using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.General.StaticUtils.Enums;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public class CellBlock : AbstractChildCellObject
    {
        public CellBlock(int id, Action<object, PerformanceParams> commitReaction) : base(id, commitReaction)
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

            ActionPerformanceParams<CellBlockViewAction> actionResult;

            switch (cellBlockAction)
            {
                case CellBlockAction.Select:
                    actionResult = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Select);
                    _commitReaction?.Invoke(this, actionResult);
                    return;
                case CellBlockAction.Unselect:
                    actionResult = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Unselect);
                    _commitReaction?.Invoke(this, actionResult);
                    return;
                default:
                    actionResult = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Error);
                    _commitReaction?.Invoke(this, actionResult);
                    throw new ArgumentOutOfRangeException(nameof(cellBlockAction), cellBlockAction, null);
            }
        }

        private void CommitBaseAction(object sender, CellObjectBaseAction baseActionType)
        {
            ActionPerformanceParams<CellBlockViewAction> actionResult;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Dispose:
                    actionResult = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Dispose);
                    _commitReaction?.Invoke(this, actionResult);
                    ParentCell?.Clear();
                    return;
                case CellObjectBaseAction.ApplyGravity:
                    actionResult = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.ApplyGravity);
                    _commitReaction?.Invoke(this, actionResult);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
        }
    }
}