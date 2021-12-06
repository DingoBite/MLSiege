using Assets.Siege.Model.CellularSpace.Fabrics;
using Assets.Siege.Model.CellularSpace.Fabrics.Interfaces;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class FabricsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IMonoBlockFabric>().To<MonoBlockFabric>().AsSingle().NonLazy();
        }
    }
}