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
            Container.Bind<IIdRepository<Vector3Int>>().To<Vector3IntIdRepository>().AsTransient().NonLazy();
        }
    }
}