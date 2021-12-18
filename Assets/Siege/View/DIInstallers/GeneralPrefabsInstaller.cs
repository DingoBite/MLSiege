using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class GeneralPrefabsInstaller : MonoInstaller
    {
        [SerializeField] private Tilemap _tilemapPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Tilemap>().FromInstance(_tilemapPrefab).AsSingle();
        }
    }
}