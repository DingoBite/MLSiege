using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Fabrics;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.View.Agents;
using Assets.Siege.View.Blocks;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class FabricsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IFrameFabric<FrameBlock, BlockInfo, MonoBlock>>().To<FrameBlockFabric>().AsSingle().NonLazy();
            Container.Bind<IFrameFabric<FrameAgent, AgentInfo, MonoAgent>>().To<FrameAgentFabric>().AsSingle().NonLazy();
        }
    }
}