using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Repositories;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Agents;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class AgentSpaceInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRepository<FrameAgent>>().To<FrameAgentRepository>().AsTransient().NonLazy();

            Container.Bind<IBlockSpace<FrameAgent, AgentInfo, MonoAgent>>().To<FrameAgentSpace>().AsSingle();
        }
    }
}