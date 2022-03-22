using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;

namespace Game.Scripts.CellularSpace.CellStorages
{
    public class Cell : ICell
    {
        public Cell(AbstractCellObject cellObject = null)
        {
            CellObject = cellObject;
        }
        
        public bool IsEmpty => CellObject != null;
        public AbstractCellObject CellObject { get; set; }
    }
}