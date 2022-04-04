using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.MultiCellObject
{
    public abstract class AbstractCellObjectMainPart : AbstractChildCellObject
    {
        private readonly int[] _partsId;

        protected AbstractCellObjectMainPart(int id, int[] partsId,
            Action<object, PerformanceParams> commitReaction,
            bool isExternallyModifiable) : base(id, commitReaction, isExternallyModifiable)
        {
            _partsId = partsId;
        }

        public override void CommitAction(object sender, PerformanceParams performanceParams)
        {
            int? actionFromPartId = null;
            var partsToAct = new List<AbstractCellObject>();
            foreach (var partId in _partsId)
            {
                if (!ParentCell.CellGrid.TryGetCellObject(partId, out var part))
                {
                    CommitBaseAction(sender, CellObjectBaseAction.Dispose, partsToAct);
                    return;
                }
                if (sender == part)
                    actionFromPartId = partId;
                partsToAct.Add(part);
            }
            
            if (actionFromPartId.HasValue)
                OnCommitFromPart(actionFromPartId.Value, performanceParams, partsToAct);
            else
                OnCommit(sender, performanceParams, partsToAct);
        }
        
        protected abstract void OnCommit(object sender, PerformanceParams performanceParams, List<AbstractCellObject> parts);
        
        protected abstract void OnCommitFromPart(int partId, PerformanceParams performanceParams, List<AbstractCellObject> parts);
        
        protected abstract void CommitBaseAction(object sender, CellObjectBaseAction baseActionType, List<AbstractCellObject> parts);
    }
}