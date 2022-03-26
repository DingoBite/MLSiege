using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public class CellAgentHead : AbstractCellObjectMainPart
    {
        public CellAgentHead(int id, int[] partsIds, Action<object, PerformanceParams> commitReaction) : base(id, partsIds, commitReaction)
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
            ActionPerformanceParams<CellBlockViewAction> actionResult;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Dispose:
                    actionResult = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.Dispose);
                    foreach (var part in parts)
                    {
                        part?.CommitAction(this, actionResult);
                    }
                    _commitReaction?.Invoke(this, actionResult);
                    ParentCell?.Clear();
                    return;
                case CellObjectBaseAction.ApplyGravity:
                    actionResult = new ActionPerformanceParams<CellBlockViewAction>(CellBlockViewAction.ApplyGravity);
                    var fullCellObject = parts.ToList();
                    fullCellObject.Add(this);
                    fullCellObject.Sort((c1, c2) => c1.Coords.y.CompareTo(c2.Coords.y));
                    foreach (var part in fullCellObject)
                    {
                        if (part == this)
                        {
                            //TODO gravity logic
                            _commitReaction?.Invoke(this, actionResult);
                        }
                        part?.CommitAction(this, actionResult);
                    }
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
        }
    }
}