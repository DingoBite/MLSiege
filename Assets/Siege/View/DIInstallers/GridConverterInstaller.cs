using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.CoordsConverters;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using Assets.Siege.Model.BlockSpace.Fabrics;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.View.Blocks;
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
            Container.Bind<IGridShaper<BlockInfo, MonoBlock>>().To<LevelGridShaper>().AsSingle().NonLazy();

            Container.Bind<IGridCoordsConverter>().To<GridCoordsConverter>().AsSingle().WithArguments(_tilemapGameObjectsGrid.cellSize).NonLazy();
            Container.Bind<IFrameFabric<FrameBlock, BlockInfo, MonoBlock>>().To<FrameBlockFabric>().AsSingle().NonLazy();

            Container.Bind<IRepository<FrameBlock>>().To<FrameBlockRepository>().AsSingle().NonLazy();
            Container.Bind<IIdRepository<Vector3Int>>().To<Vector3IntIdRepository>().AsSingle().NonLazy();

            Container.Bind<IBlockSpace<FrameBlock, BlockInfo, MonoBlock>>().To<BlockSpace>().AsSingle();
        }
    }
}