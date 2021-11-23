using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IGridShaper
    {
        public void Shape(Grid grid, IBlockSpace blockSpace);
    }
}