using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Fabrics;
using Assets.Siege.CellularSpace.Fabrics.Interfaces;
using Assets.Siege.View.Agents;
using Assets.Siege.View.Blocks;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class FabricsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFrameFabric<FrameBlock, BlockInfo, MonoBlock>>().To<FrameBlockFabric>().AsTransient();
            Container.Bind<IFrameFabric<FrameAgent, AgentInfo, MonoAgent>>().To<FrameAgentFabric>().AsTransient();
            Container.Bind<IMonoFabric<MonoBlock, Block>>().To<MonoBlockFabric>().AsTransient();
            Container.Bind<IMonoFabric<MonoAgent, Agent>>().To<MonoAgentFabric>().AsTransient();
        }
    }
}