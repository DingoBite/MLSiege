using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    internal interface IGridShaper
    {
        public void Shape(Grid grid, IBlockSpace blockSpace);
    }
}