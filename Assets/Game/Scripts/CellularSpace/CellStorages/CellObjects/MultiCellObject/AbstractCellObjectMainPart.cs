using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public abstract class AbstractCellObjectMainPart : AbstractChildCellObject
    {
        private readonly int[] _partsId;

        public AbstractCellObjectMainPart(int id, int[] partsId, Action<object, PerformanceParams> commitReaction) : base(id, commitReaction)
        {
            _partsId = partsId;
        }

        public override void CommitAction(object sender, PerformanceParams performanceParams)
        {
            var isSuccessfulState = true;
            var partsToAct = new List<AbstractCellObject>();
            foreach (var partId in _partsId)
            {
                if (!ParentCell.CellGridContext.TryGetCellObject(partId, out var part))
                    isSuccessfulState = false;
                partsToAct.Add(part);
            }

            if (!isSuccessfulState)
            {
                CommitBaseAction(sender, CellObjectBaseAction.Dispose, partsToAct);
                return;
            }
            
            foreach (var part in partsToAct)
            {
                part.CommitAction(this, performanceParams);
            }

            OnCommit(sender, performanceParams, partsToAct);
        }
        
        protected abstract void OnCommit(object sender, PerformanceParams performanceParams, List<AbstractCellObject> parts);
        
        protected abstract void CommitBaseAction(object sender, CellObjectBaseAction baseActionType, List<AbstractCellObject> parts);
    }
}