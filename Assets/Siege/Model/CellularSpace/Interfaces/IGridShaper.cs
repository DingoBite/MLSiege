namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IGridShaper
    {
        public void Shape(IGameObjectGrid gameObjectGrid, IBlockSpace blockSpace);
    }
}