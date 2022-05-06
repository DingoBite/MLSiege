using Game.Scripts.Agents;
using Game.Scripts.Agents.Interfaces;
using UnityEngine;
using Zenject;

public class MLBehaviorModulesInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IAgentManager>().To<AgentManager>().AsTransient();
        Container.Bind<IObservationsCollector>().To<ObservationsCollector>().AsTransient();
        Container.Bind<IActionResolver>().To<ActionResolver>().AsTransient();
    }
}