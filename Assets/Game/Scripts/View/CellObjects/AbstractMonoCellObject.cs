using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.View.CellObjects.Interfaces;
using UnityEngine;

namespace Game.Scripts.View.CellObjects
{
    public abstract class AbstractMonoCellObject: MonoBehaviour, IActable<FlexibleData>
    {
        public abstract AbstractCellObject GetCellObject();
        protected abstract CellObjectType GetCellObjectType();
        public abstract void CommitAction(FlexibleData cellObjectFuncResult);
    }
}
