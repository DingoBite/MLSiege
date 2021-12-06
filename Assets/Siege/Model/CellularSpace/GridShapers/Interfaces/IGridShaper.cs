using Assets.Siege.Model.CellularSpace.Interfaces;

namespace Assets.Siege.Model.CellularSpace.GridShapers.Interfaces
{
    public interface IGridShaper
    {
        public void Shape(IGameObjectGrid gameObjectGrid, IBlockSpaceController blockSpace);
    }
}