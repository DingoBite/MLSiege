using Game.Scripts.TurnManager;
using Game.Scripts.TurnManager.Interfaces;
using UnityEngine;
using Zenject;

public class TurnManagerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<ITurnManager>().To<WrappedTurnManager>().AsTransient();
    }
}