using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;

namespace Assets.Siege.CellularSpace.Repositories.Interfaces
{
    public interface IFrameSpace<TFrame, in TInfo, TMono> : IFrameSpaceContext<TFrame>, IFrameSpaceDataController<TInfo, TMono>
        where TMono : CellularSpaceMonoObject<TInfo>
    {
        public void Init(ITilemapLevelsGrid<TMono> tilemapLevelsGrid);
    }
}