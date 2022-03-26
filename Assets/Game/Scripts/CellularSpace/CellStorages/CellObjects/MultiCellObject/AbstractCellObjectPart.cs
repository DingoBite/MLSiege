using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public abstract class AbstractCellObjectPart : AbstractChildCellObject
    {
        private readonly int _mainPartId;

        protected AbstractCellObjectPart(int id, 
            int mainPartId, Action<object, PerformanceParams> commitReaction) : base(id, commitReaction, false)
        {
            _mainPartId = mainPartId;
        }
        
        public override void CommitAction(object sender, PerformanceParams performanceParams)
        {
            if (!ParentCell.CellGridContext.TryGetCellObject(_mainPartId, out var mainPart))
            {
                CommitBaseAction(sender, CellObjectBaseAction.Dispose);
                return;
            }
            if (sender != mainPart) return;

            OnCommit(sender, performanceParams);
        }

        protected abstract void CommitBaseAction(object sender, CellObjectBaseAction baseActionType);

        protected abstract void OnCommit(object sender, PerformanceParams performanceParams);
    }
}