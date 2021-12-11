using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.CoordsConverters;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using Assets.Siege.Model.BlockSpace.Fabrics;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.View.Agents;
using Assets.Siege.View.Blocks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class GridConverterInstaller : MonoInstaller
    {
        [SerializeField] private Grid _tilemapBlockGrid;
        [SerializeField] private Grid _tilemapAgentGrid;
        [SerializeField] private Tilemap _tilemapPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Tilemap>().FromInstance(_tilemapPrefab).AsSingle().NonLazy();
            Container.Bind<ITilemapLevelsGrid>().To<TilemapLevelsGrid>().AsTransient().NonLazy();
            Container.Bind<IGridShaper<BlockInfo, MonoBlock>>().To<BlockGridShaper>().AsSingle()
                .WithArguments(_tilemapBlockGrid).NonLazy();
            Container.Bind<IGridShaper<AgentInfo, MonoAgent>>().To<AgentGridShaper>().AsSingle()
                .WithArguments(_tilemapAgentGrid).NonLazy();

            Container.Bind<IMonoFabric<MonoBlock, Block>>().To<MonoBlockFabric>().AsSingle()
                .WithArguments(_tilemapBlockGrid).NonLazy();
            Container.Bind<IMonoFabric<MonoAgent, Agent>>().To<MonoAgentFabric>().AsSingle()
                .WithArguments(_tilemapAgentGrid).NonLazy();

            Container.Bind<IGridCoordsConverter>().To<GridCoordsConverter>().AsSingle().WithArguments(_tilemapBlockGrid.cellSize).NonLazy();
        }
    }
}