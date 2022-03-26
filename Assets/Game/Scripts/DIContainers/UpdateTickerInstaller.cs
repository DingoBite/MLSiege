using Game.Scripts.Time;
using Game.Scripts.Time.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts.DIContainers
{
    public class UpdateTickerInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_updateTicker")] [SerializeField] private UpdateTicker abstractUpdateTicker;
    
        public override void InstallBindings()
        {
            Container.Bind<IUpdateTicker>().FromInstance(abstractUpdateTicker).AsSingle();
        }
    }
}