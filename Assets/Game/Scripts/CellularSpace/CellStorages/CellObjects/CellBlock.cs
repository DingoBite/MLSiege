using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public class CellBlock : AbstractCellObject
    {
        public CellBlock(int id, Action<FlexibleData> commitReaction, Action disposeAction) : base(id, commitReaction, disposeAction)
        {
        }

        public override void CommitAction(FlexibleData flexibleData)
        {
            if (!(flexibleData is ActionPerformanceData<CellBlockLogicAction> performanceData))
            {
                if (flexibleData is ActionPerformanceData<CellObjectBaseAction> data)
                    CommitAction(data.ActionType);
                return;
            }

            ActionPerformanceData<CellBlockViewAction> actionResult;
            
            switch (performanceData.ActionType)
            {
                case CellBlockLogicAction.Select:
                    actionResult = new ActionPerformanceData<CellBlockViewAction>(CellBlockViewAction.Select);
                    _commitReaction?.Invoke(actionResult);
                    return;
                case CellBlockLogicAction.Unselect:
                    actionResult = new ActionPerformanceData<CellBlockViewAction>(CellBlockViewAction.Unselect);
                    _commitReaction?.Invoke(actionResult);
                    return;
                default:
                    actionResult = new ActionPerformanceData<CellBlockViewAction>(CellBlockViewAction.Error);
                    _commitReaction?.Invoke(actionResult);
                    throw new ArgumentOutOfRangeException(nameof(performanceData.ActionType), performanceData.ActionType, null);
            }
        }

        public override void CommitAction(CellObjectBaseAction baseActionType)
        {
            ActionPerformanceData<CellBlockViewAction> actionResult;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Dispose:
                    var disposeResult = new ActionPerformanceData<CellBlockViewAction>(CellBlockViewAction.Dispose);
                    _commitReaction?.Invoke(disposeResult);
                    _disposeAction?.Invoke();
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
        }
    }
}