using Game.Scripts.SiegeML;
using Game.Scripts.SiegeML.ActionResolvers;
using Game.Scripts.SiegeML.ObservationsCollectors;
using Zenject;

namespace Game.Scripts.DIContainers
{
    public class MLBehaviorModulesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAgentManager>().To<AgentManager>().AsTransient();
            Container.Bind<IObservationsCollector>().To<ObservationsCollector>().AsTransient();
            Container.Bind<IActionResolver>().To<ActionResolver>().AsTransient();
        }
    }
}