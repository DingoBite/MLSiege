using Game.Scripts.Time;
using Game.Scripts.Time.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.DIContainers
{
    public class UpdateTickerInstaller : MonoInstaller
    {
        [SerializeField] private IUpdateTicker _updateTicker;
        [SerializeField] private IOneActUpdateTicker _oneActUpdateTicker;
        
        public override void InstallBindings()
        {
            Container.Bind<IUpdateTicker>().FromInstance(_updateTicker).AsSingle();
            Container.Bind<IOneActUpdateTicker>().FromInstance(_oneActUpdateTicker).AsSingle();
        }
    }
}