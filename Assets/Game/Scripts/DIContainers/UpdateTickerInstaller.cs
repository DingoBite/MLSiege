using Game.Scripts.Time;
using Game.Scripts.Time.Interfaces;
using UnityEngine;
using Zenject;

public class UpdateTickerInstaller : MonoInstaller
{
    [SerializeField] private UpdateTicker _updateTicker;
    
    public override void InstallBindings()
    {
        Container.Bind<IUpdateTicker>().FromInstance(_updateTicker).AsSingle();
    }
}