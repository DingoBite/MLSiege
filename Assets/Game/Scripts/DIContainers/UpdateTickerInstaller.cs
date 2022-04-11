using Game.Scripts.Time;
using Game.Scripts.Time.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts.DIContainers
{
    public class UpdateTickerInstaller : MonoInstaller
    {
        [SerializeField] private UpdateTicker _updateTicker;
    
        public override void InstallBindings()
        {
            Container.Bind<UpdateTicker>().FromInstance(_updateTicker).AsSingle();
        }
    }
}