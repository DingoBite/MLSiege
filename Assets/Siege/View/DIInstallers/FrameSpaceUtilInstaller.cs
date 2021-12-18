using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.CoordsConverters;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
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