using Game.Scripts.CellularSpace.CellStorages.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.General.Interfaces;

namespace Game.Scripts.CellularSpace.CellStorages.CellObjects
{
    public abstract class AbstractCellObject : IFunctionable<FlexibleData>
    {
        protected ICharacteristics _characteristics;

        public CellObjectType CellObjectType => _characteristics.CellObjectType;
        public abstract FlexibleData CommitAction(FlexibleData flexibleData);
    }
}