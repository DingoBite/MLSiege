using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Blocks.Enums;
using Assets.Siege.CellularSpace.Fabrics.Interfaces;
using Assets.Siege.CellularSpace.General.Interfaces;
using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.CellularSpace.Fabrics
{
    public class MonoBlockFabric: IMonoFabric<MonoBlock, Block>
    {
        private readonly IPrefabsByType<BlockType, MonoBlock> _blockPrefabs;
        private ITilemapLevelsGrid<MonoBlock> _tilemapLevelsGrid;

        [Inject]
        public MonoBlockFabric(IPrefabsByType<BlockType, MonoBlock> blockPrefabs)
        {
            _blockPrefabs = blockPrefabs;
        }

        public void Init(ITilemapLevelsGrid<MonoBlock> tilemapLevelsGrid)
        {
            _tilemapLevelsGrid = tilemapLevelsGrid;
        }

        public MonoBlock Make(int id, int level, Vector3 position, Block block)
        {
            var monoBlockPrefab = _blockPrefabs[block.Features.BlockType];
            return _tilemapLevelsGrid.PutIntoLevel(level, monoBlockPrefab, position, id);
        }
    }
}