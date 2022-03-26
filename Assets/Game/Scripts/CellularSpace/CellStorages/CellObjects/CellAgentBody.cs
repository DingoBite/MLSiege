using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public class CellAgentBody : AbstractCellObjectPart
    {
        public CellAgentBody(int id, int mainId, Action<object, PerformanceParams> commitReaction) 
            : base(id, mainId, commitReaction)
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

        protected override void CommitBaseAction(object sender, CellObjectBaseAction baseActionType)
        {
            ActionPerformanceParams<CellBlockViewAction> actionResult;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Dispose:
                    actionResult = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Dispose);
                    _commitReaction?.Invoke(this, actionResult);
                    ParentCell?.Clear();
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
        }
    }
}