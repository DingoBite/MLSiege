﻿namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockSpace: IBlockSpaceContext, IBlockSpaceController
    {
        public void Clear();
    }
}