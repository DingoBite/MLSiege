using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.MultiCellObject;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations
{
    public class CellAgentLegs : AbstractCellObjectPart
    {
        public CellAgentLegs(int id, int mainPartId, 
            Action<object, PerformanceParams> commitReaction, bool isExternallyModifiable) 
            : base(id, mainPartId, commitReaction, isExternallyModifiable)
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
        }

        protected override void CommitBaseAction(object sender, CellObjectBaseAction baseActionType)
        {
            ActionPerformanceParams<CellAgentViewAction> viewActionPerformanceParams;
            switch (baseActionType)
            {
                case CellObjectBaseAction.Select:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.Select);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    return;
                case CellObjectBaseAction.Unselect:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.Unselect);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    return;
                case CellObjectBaseAction.Dispose:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.Dispose);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    ParentCell?.Clear();
                    return;
                case CellObjectBaseAction.ApplyGravity:
                    viewActionPerformanceParams = new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.ApplyGravity);
                    viewActionPerformanceParams.FlexibleData.Vector3IntParams.SetParam("NewCoords", Coords);
                    _commitReaction?.Invoke(this, viewActionPerformanceParams);
                    return;
                default:
                    throw new ArgumentOutOfRangeException(nameof(baseActionType), baseActionType, null);
            }
        }
    }
}