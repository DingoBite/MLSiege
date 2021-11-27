using Assets.Siege.Model.CellularSpace.Fabrics;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Zenject;

namespace Assets.Siege.MonoBehaviors.DIInstallers
{
    public class FabricsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBlockFabric>().To<BlockFabric>().AsSingle().NonLazy();
            Container.Bind<IMonoBlockFabric>().To<MonoBlockFabric>().AsSingle().NonLazy();
        }
    }
}