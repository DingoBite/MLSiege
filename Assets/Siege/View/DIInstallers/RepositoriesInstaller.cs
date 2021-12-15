using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories;
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
            Container.Bind<IIdRepository<Vector3Int>>().To<Vector3IntIdRepository>().AsTransient();
        }
    }
}