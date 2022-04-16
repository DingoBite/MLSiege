using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.Realizations.ComplexCellObject
{
    public abstract class AbstractCellObjectMainPart : AbstractChildCellObject
    {
        private readonly int[] _partsId;

        protected AbstractCellObjectMainPart(int id, IEnumerable<int> partsId,
            Action<object, PerformanceParam> commitReaction,
            bool isModifiable) : base(id, commitReaction, isModifiable)
        {
            _partsId = partsId.ToArray();
        }

        public override bool CommitAction(object sender, PerformanceParam performanceParam)
        {
            if (_partsId == null) 
                throw new Exception("Parts are not initialized");
            AbstractCellObject actionFromPart = null;
            var partsToAct = new List<AbstractCellObject>();
            foreach (var partId in _partsId)
            {
                if (!ParentCell.CellGrid.TryGetCellObject(partId, out var part))
                    return CommitBaseAction(sender, performanceParam, CellObjectBaseAction.Dispose, partsToAct);
                
                if (sender == part)
                    actionFromPart = part;
                partsToAct.Add(part);
            }
            
            return actionFromPart != null ? OnCommitFromPart(actionFromPart, performanceParam, partsToAct) 
                : OnCommit(sender, performanceParam, partsToAct);
        }
        
        protected abstract bool OnCommit(object sender, PerformanceParam performanceParam, List<AbstractCellObject> parts);
        
        protected abstract bool OnCommitFromPart(AbstractCellObject part, PerformanceParam performanceParam, List<AbstractCellObject> parts);
        
        protected abstract bool CommitBaseAction(object sender, PerformanceParam performanceParam, 
            CellObjectBaseAction baseActionType, List<AbstractCellObject> parts);
    }
}