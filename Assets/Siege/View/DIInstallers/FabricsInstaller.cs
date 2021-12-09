using Assets.Siege.Model.BlockSpace.Fabrics;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.View.Blocks;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class FabricsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMonoFabric<MonoBlock>>().To<MonoBlockFabric>().AsSingle().NonLazy();
        }
    }
}