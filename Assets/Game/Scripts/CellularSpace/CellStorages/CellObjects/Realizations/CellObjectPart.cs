using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.MultiCellObject
{
    public class CellObjectPart : AbstractCellObjectPart
    {
        public CellObjectPart(int id, int mainId,
            Action<object, PerformanceParams> commitReaction,
            bool isExternallyModifiable) : base(id, mainId, commitReaction, isExternallyModifiable)
        {
        }

        protected override void OnCommit(object sender, PerformanceParams performanceParams)
        {
            if (!(performanceParams.RawActionType is CellAgentAction cellAgentAction))
            {
                if (performanceParams.RawActionType is CellObjectBaseAction cellAgentBaseAction)
                    CommitBaseAction(sender, cellAgentBaseAction);
                
                return;
            }
            ActionPerformanceParams<CellAgentAction> viewActionPerformanceParams;

            switch (cellAgentAction)
            {
                case CellAgentAction.Select:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellAgentAction>(CellAgentAction.Select);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    break;
                case CellAgentAction.Unselect:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellAgentAction>(CellAgentAction.Unselect);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override void CommitBaseAction(object sender, CellObjectBaseAction baseActionType)
        {
            ActionPerformanceParams<CellBlockViewAction> viewActionPerformanceParams;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Dispose:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Dispose);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    ParentCell?.Clear();
                    return;
                case CellObjectBaseAction.ApplyGravity:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.ApplyGravity);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
        }
    }
}