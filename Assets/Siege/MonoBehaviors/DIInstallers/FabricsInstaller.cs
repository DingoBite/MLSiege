using Assets.Siege.Model.CellularSpace.Fabrics;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks.Fabrics;
using Assets.Siege.Model.ObjectFeatures.Interfaces;
using Zenject;

namespace Assets.Siege.MonoBehaviors.DIInstallers
{
    public class FabricsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IBlockFeaturesFabric>().To<BlockFeaturesFabric>().AsSingle().NonLazy();
            Container.Bind<IBlockFabric>().To<BlockFabric>().AsSingle().NonLazy();
            Container.Bind<IMonoBlockFabric>().To<MonoBlockFabric>().AsSingle().NonLazy();
        }
    }
}