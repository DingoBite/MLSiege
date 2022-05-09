using Game.Scripts.CellObjects;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellMutable : ICell
    {
        void SetCellObject(AbstractChildCellObject childCellObject);
        AbstractChildCellObject ChildCellObject { get; }
    }
}