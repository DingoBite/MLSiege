using Game.Scripts.CellularSpace.CellStorages.CellObjects;

namespace Game.Scripts.CellularSpace.CellStorages.Interfaces
{
    public interface ICell
    {
        bool IsEmpty { get; }
        AbstractCellObject CellObject { get; set; }
    }
}