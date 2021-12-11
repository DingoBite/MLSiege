using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.Fabrics
{
    public class MonoBlockFabric: IMonoFabric<MonoBlock, Block>
    {
        private readonly IDictionary<BlockType, MonoBlock> _blockPrefabs;
        private readonly ITilemapLevelsGrid _tilemapLevelsGrid;

        public MonoBlockFabric([Inject] IDictionary<BlockType, MonoBlock> blockPrefabs, [Inject] ITilemapLevelsGrid tilemapLevelsGrid, Grid grid)
        {
            _blockPrefabs = blockPrefabs;
            _tilemapLevelsGrid = tilemapLevelsGrid;
            _tilemapLevelsGrid.Init(grid);
        }

        public MonoBlock Make(int id, int level, Vector3 position, Block block)
        {
            var monoBlockPrefab = _blockPrefabs[block.Features.BlockType];
            var gameObjectMonoBlock = Object.Instantiate(monoBlockPrefab, position, Quaternion.identity, _tilemapLevelsGrid.GetLevel(level).transform);
            var monoBlock = gameObjectMonoBlock.GetComponent<MonoBlock>();
            monoBlock.Id = id;
            gameObjectMonoBlock.name = monoBlock.ToString();
            return monoBlock;
        }
    }
}