﻿namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IGridShaper
    {
        public void Shape(IGridHierarchy gridHierarchy, IBlockSpace blockSpace);
    }
}