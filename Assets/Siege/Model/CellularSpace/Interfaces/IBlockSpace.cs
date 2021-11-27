namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockSpace: IBlockSpaceContext, IPackBlockSpaceContext, IBlockSpaceController
    {
        public void Clear();
    }
}