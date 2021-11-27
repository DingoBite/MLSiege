using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Converters;
using Assets.Siege.Model.CellularSpace.Fabrics;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.CellularSpace.Repositories;
using Assets.Siege.Model.CellularSpace.Shapers;
using Assets.Siege.Model.General.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Siege.MonoBehaviors.DIInstallers
{
    public class GridConverterInstaller : MonoInstaller
    {
        [SerializeField] private Grid _tilemapGameObjectsGrid;
        [SerializeField] private Tilemap _tilemapPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Tilemap>().FromInstance(_tilemapPrefab).AsSingle().NonLazy();
            Container.Bind<Grid>().FromInstance(_tilemapGameObjectsGrid).AsTransient().NonLazy();
            Container.Bind<IGameObjectGrid>().To<BlockLevelsGrid>().AsSingle().NonLazy();
            Container.Bind<IGridShaper>().To<LevelGridShaper>().AsSingle().NonLazy();

            Container.Bind<IRepository<PackBlock>>().To<PackBlockRepository>().AsTransient().NonLazy();
            Container.Bind<IIdRepository<Vector3Int>>().To<Vector3IntIdRepository>().AsTransient().NonLazy();

            Container.Bind<IGridCoordsConverter>().To<HandGridCoordsConverter>().AsSingle().NonLazy();
            Container.Bind<IPackBlockFabric>().To<PackBlockFabric>().AsSingle().NonLazy();

            Container.Bind<IBlockSpace>().To<BlockSpace>().AsSingle();
        }
    }
}