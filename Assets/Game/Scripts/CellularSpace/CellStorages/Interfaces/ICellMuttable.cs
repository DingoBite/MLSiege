using Game.Scripts.CellularSpace.CellStorages.CellObjects;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICellMutable : ICell
    {
        void SetCellObject(AbstractChildCellObject childCellObject);
    }
}