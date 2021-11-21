using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.General.CellularSpace.CellularSpaceGenerator.Interfaces
{
    public interface ICellableGrid
    {
        public bool TryGetCellableMono(int id, out CellableMono cellableMono);
        public void DeleteCellableMono(int id);
        public bool TryUpdateCellableMono(int id, CellableMono newCellableMono);
    }
}