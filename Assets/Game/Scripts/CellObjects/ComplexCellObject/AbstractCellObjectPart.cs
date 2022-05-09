using System;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellObjects.ComplexCellObject
{
    public abstract class AbstractCellObjectPart : AbstractChildCellObject
    {
        protected readonly int _mainPartId;

        protected AbstractCellObjectPart(int id, int mainPartId, Action<object, PerformanceParam> commitReaction,
            bool isModifiable) : base(id, commitReaction, isModifiable)
        {
            _mainPartId = mainPartId;
        }

        public override bool CommitAction(object sender, PerformanceParam performanceParam)
        {
            if (!ParentCellMutable.CellGrid.TryGetCellObject(_mainPartId, out var mainPart))
                return CommitBaseAction(sender, performanceParam, CellObjectBaseAction.Dispose);

            return sender != mainPart ? mainPart.CommitAction(this, performanceParam) 
                : OnCommit(sender, performanceParam);
        }
        
        protected abstract bool OnCommit(object sender, PerformanceParam performanceParam);
        
        protected abstract bool CommitBaseAction(object sender, PerformanceParam performanceParam, CellObjectBaseAction baseActionType);
    }
}