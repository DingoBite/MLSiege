using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.General.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
{
    public abstract class AbstractMonoCellObject: MonoBehaviour, IActable<FlexibleData>
    {
        protected abstract CellObjectType GetCellObjectType();
        public abstract void CommitAction(FlexibleData cellObjectFuncResult);
    }
}
