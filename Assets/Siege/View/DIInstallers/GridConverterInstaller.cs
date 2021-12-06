using Assets.Siege.Model.CellularSpace.Converters;
using Assets.Siege.Model.CellularSpace.Converters.Interfaces;
using Assets.Siege.Model.CellularSpace.Fabrics;
using Assets.Siege.Model.CellularSpace.Fabrics.Interfaces;
using Assets.Siege.Model.CellularSpace.GridShapers;
using Assets.Siege.Model.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.CellularSpace.Repositories;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Siege.View.DIInstallers
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

            Container.Bind<IGridCoordsConverter>().To<GridCoordsConverter>().AsSingle().WithArguments(_tilemapGameObjectsGrid.cellSize).NonLazy();
            Container.Bind<IPackBlockFabric>().To<PackBlockFabric>().AsSingle().NonLazy();

            Container.Bind<IBlockSpace>().To<BlockSpace>().AsSingle();
        }
    }
}