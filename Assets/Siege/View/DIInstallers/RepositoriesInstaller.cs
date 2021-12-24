using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.General.Interfaces;
using Assets.Siege.CellularSpace.Repositories;
using UnityEngine;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class RepositoriesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRepository<FrameAgent>>().To<FrameRepository<FrameAgent>>().AsTransient();
            Container.Bind<IRepository<FrameBlock>>().To<FrameRepository<FrameBlock>>().AsTransient();
            Container.Bind<IRepository<int, Vector3Int>>().To<IdVectorRepository>().AsTransient();
            Container.Bind<IRepository<Vector3Int, int>>().To<VectorIdRepository>().AsTransient();
        }
    }
}