using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using TMPro;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.MultiCellObject
{
    public abstract class AbstractCellObjectMainPart : AbstractChildCellObject
    {
        private int[] _partsId;

        protected AbstractCellObjectMainPart(int id, IEnumerable<int> partsId,
            Action<object, PerformanceParams> commitReaction,
            bool isExternallyModifiable) : base(id, commitReaction, isExternallyModifiable)
        {
            _partsId = partsId.ToArray();
        }
        
        protected AbstractCellObjectMainPart(int id, Action<object, PerformanceParams> commitReaction,
            bool isExternallyModifiable) : base(id, commitReaction, isExternallyModifiable)
        {
        }

        public void PartsInit(IEnumerable<int> partsId)
        {
            if (_partsId != null)
            {
                var tempPartsId = _partsId.ToArray();
                _partsId = null;
                foreach (var partId in tempPartsId)
                {
                    if (!ParentCell.CellGrid.TryGetCellObject(partId, out var part)) continue;
                    part.CommitAction(this, new ActionPerformanceParams<CellAgentViewAction>(CellAgentViewAction.Dispose));
                }
            }
            _partsId = partsId.ToArray();
        }
        
        public override void CommitAction(object sender, PerformanceParams performanceParams)
        {
            if (_partsId == null) 
                throw new Exception("Parts are not initialized");
            AbstractCellObject actionFromPart = null;
            var partsToAct = new List<AbstractCellObject>();
            foreach (var partId in _partsId)
            {
                if (!ParentCell.CellGrid.TryGetCellObject(partId, out var part))
                {
                    CommitBaseAction(sender, CellObjectBaseAction.Dispose, partsToAct);
                    return;
                }
                if (sender == part)
                    actionFromPart = part;
                partsToAct.Add(part);
            }
            
            if (actionFromPart != null)
                OnCommitFromPart(actionFromPart, performanceParams, partsToAct);
            else
                OnCommit(sender, performanceParams, partsToAct);
        }
        
        protected abstract void OnCommit(object sender, PerformanceParams performanceParams, List<AbstractCellObject> parts);
        
        protected abstract void OnCommitFromPart(AbstractCellObject part, PerformanceParams performanceParams, List<AbstractCellObject> parts);
        
        protected abstract void CommitBaseAction(object sender, CellObjectBaseAction baseActionType, List<AbstractCellObject> parts);
    }
}