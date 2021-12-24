using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.CoordsConverters;
using Assets.Siege.CellularSpace.CoordsConverters.Interfaces;
using Assets.Siege.CellularSpace.GridShapers;
using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.CellularSpace.Repositories;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.Agents;
using Assets.Siege.View.Blocks;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class FrameSpaceUtilInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGridCoordsConverter>().To<GridCoordsConverter>().AsTransient();

            Container.Bind<ITilemapLevelsGrid<MonoBlock>>().To<TilemapLevelsGrid<MonoBlock>>().AsTransient();
            Container.Bind<ITilemapLevelsGrid<MonoAgent>>().To<TilemapLevelsGrid<MonoAgent>>().AsTransient();

            Container.Bind<IGridShaper<BlockInfo, MonoBlock>>().To<GridShaper<BlockInfo, MonoBlock>>().AsTransient();
            Container.Bind<IGridShaper<AgentInfo, MonoAgent>>().To<GridShaper<AgentInfo, MonoAgent>>().AsTransient();

            Container.Bind<IFrameSpace<FrameBlock, BlockInfo, MonoBlock>>().To<FrameSpace<FrameBlock, BlockInfo, MonoBlock>>().AsTransient();
            Container.Bind<IFrameSpace<FrameAgent, AgentInfo, MonoAgent>>().To<FrameSpace<FrameAgent, AgentInfo, MonoAgent>>().AsTransient();
        }
    }
}