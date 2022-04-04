using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.MultiCellObject;
using Game.Scripts.General.FlexibleDataApi;

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
            throw new NotImplementedException();
        }

        protected override void CommitBaseAction(object sender, CellObjectBaseAction baseActionType)
        {
            throw new NotImplementedException();
        }
    }
}