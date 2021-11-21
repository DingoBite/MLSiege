using UnityEngine;
using Zenject;

namespace Assets.Siege.MonoBehaviors.DIInstallers
{
    public class TilemapConverterInstaller : MonoInstaller
    {
        [SerializeField] private Grid _tilemapGrid;
        public override void InstallBindings()
        {
        }
    }
}