using Game.Scripts.Time.TurnManager;
using Game.Scripts.Time.TurnManager.Interfaces;
using Zenject;

namespace Game.Scripts.DIContainers
{
    public class TurnManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITurnManager>().To<WrappedTurnManager>().AsTransient();
        }
    }
}