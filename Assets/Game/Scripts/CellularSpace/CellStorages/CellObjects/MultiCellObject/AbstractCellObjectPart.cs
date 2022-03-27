using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects.MultiCellObject
{
    public abstract class AbstractCellObjectPart : AbstractChildCellObject
    {
        private readonly int _mainPartId;

        protected AbstractCellObjectPart(int id, int mainPartId,
            Action<object, PerformanceParams> commitReaction,
            bool isExternallyModifiable) : base(id, commitReaction, isExternallyModifiable)
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

            if (sender != mainPart)
            {
                Debug.Log("Try to commit action not from main part");
                return;
            }

            OnCommit(sender, performanceParams);
        }

        protected abstract void CommitBaseAction(object sender, CellObjectBaseAction baseActionType);

        protected abstract void OnCommit(object sender, PerformanceParams performanceParams);
    }
}