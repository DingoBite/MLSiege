using Game.Scripts.CellularSpace;
using Zenject;

namespace Game.Scripts.DIContainers
{
    public class GridFacadeInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGridFacade>().To<GridFacade>().AsTransient();
        }
    }
}