using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Converters;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.CellularSpace.Repositories;
using Assets.Siege.Model.CellularSpace.Shapers;
using Assets.Siege.Model.General.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Siege.MonoBehaviors.DIInstallers
{
    public class GridConverterInstaller : MonoInstaller
    {
        [SerializeField] private Grid _tilemapGameObjectsGrid;

        public override void InstallBindings()
        {
            Container.Bind<Grid>().FromInstance(_tilemapGameObjectsGrid).AsSingle();
            Container.Bind<IGridShaper>().To<GridShaper>().AsSingle().NonLazy();

            Container.Bind<IRepository<OverallBlock>>().To<OverallBlockRepository>().AsTransient().NonLazy();
            Container.Bind<IIdRepository<Vector3Int>>().To<Vector3IntIdRepository>().AsTransient().NonLazy();

            Container.Bind<IGridCoordsConverter>().To<GridCoordsConverter>().AsTransient().NonLazy();
            Container.Bind<IBlockConverter>().To<BlockConverter>().AsSingle().NonLazy();


            Container.Bind<IBlockSpace>().To<BlockSpace>().AsSingle();
        }
    }
}