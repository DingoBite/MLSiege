using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.CoordsConverters;
using Assets.Siege.Model.BlockSpace.Fabrics;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.GridShapers;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Agents;
using Assets.Siege.View.Blocks;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class CellularSpacesInstaller : MonoInstaller
    {
        [SerializeField] private Grid _tilemapBlockGrid;
        [SerializeField] private Grid _tilemapAgentGrid;
        [SerializeField] private Tilemap _tilemapPrefab;

        public override void InstallBindings()
        {
            Container.Bind<Tilemap>().FromInstance(_tilemapPrefab).AsSingle();
            Container.Bind<ITilemapLevelsGrid<MonoAgent>>().To<TilemapLevelsGrid<MonoAgent>>().AsSingle()
                .WithArguments(_tilemapAgentGrid);
            Container.Bind<ITilemapLevelsGrid<MonoBlock>>().To<TilemapLevelsGrid<MonoBlock>>().AsSingle()
                .WithArguments(_tilemapBlockGrid);
            Container.Bind<IGridShaper<BlockInfo, MonoBlock>>().To<GridShaper<BlockInfo, MonoBlock>>().AsSingle();
            Container.Bind<IGridShaper<AgentInfo, MonoAgent>>().To<GridShaper<AgentInfo, MonoAgent>>().AsSingle();

            Container.Bind<IMonoFabric<MonoBlock, Block>>().To<MonoBlockFabric>().AsSingle().NonLazy();
            Container.Bind<IMonoFabric<MonoAgent, Agent>>().To<MonoAgentFabric>().AsSingle().NonLazy();

            var blockGridCoordsConverter = new GridCoordsConverter(_tilemapBlockGrid.cellSize);
            var agentGridCoordsConverter = new GridCoordsConverter(_tilemapAgentGrid.cellSize);
            Container.Bind<IFrameSpace<FrameBlock, BlockInfo, MonoBlock>>()
                .To<FrameSpace<FrameBlock, BlockInfo, MonoBlock>>().AsSingle()
                .WithArguments(blockGridCoordsConverter).NonLazy();
            Container.Bind<IFrameSpace<FrameAgent, AgentInfo, MonoAgent>>()
                .To<FrameSpace<FrameAgent, AgentInfo, MonoAgent>>().AsSingle()
                .WithArguments(agentGridCoordsConverter).NonLazy();
        }
    }
}