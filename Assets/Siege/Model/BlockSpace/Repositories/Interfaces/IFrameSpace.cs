using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;

namespace Assets.Siege.Model.BlockSpace.Repositories.Interfaces
{
    public interface IFrameSpace<TFrame, in TInfo, TMono> : IFrameSpaceContext<TFrame>, IFrameSpaceDataController<TInfo, TMono>
        where TMono : FrameSpaceMonoObject<TInfo>
    {
        public void Init(ITilemapLevelsGrid<TMono> tilemapLevelsGrid);
    }
}