using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks.Abstracts;

namespace Assets.Siege.Model.BlockSpace.GridShapers.Interfaces
{
    public interface IGridShaper<out TInfo, out TMono> where TMono : BlockSpaceMonoObject<TInfo>
    {
        public void Shape(IGameObjectGrid gameObjectGrid, IBlockSpaceController<TInfo, TMono> blockSpace);
    }
}