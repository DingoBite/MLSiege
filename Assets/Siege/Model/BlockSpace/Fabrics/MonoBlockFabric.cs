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
    public class MonoBlockFabric: IMonoFabric<MonoBlock>
    {
        private readonly IDictionary<BlockType, MonoBlock> _blockPrefabs;
        private readonly IGameObjectGrid _gameObjectGrid;

        [Inject]
        public MonoBlockFabric(IDictionary<BlockType, MonoBlock> blockPrefabs, IGameObjectGrid gameObjectGrid)
        {
            _blockPrefabs = blockPrefabs;
            _gameObjectGrid = gameObjectGrid;
        }

        public MonoBlock Make(int id, int level, Vector3 position, Block block)
        {
            var monoBlockPrefab = _blockPrefabs[block.Features.BlockType];
            var gameObjectMonoBlock = Object.Instantiate(monoBlockPrefab, position, Quaternion.identity, _gameObjectGrid.GetLevel(level).transform);
            var monoBlock = gameObjectMonoBlock.GetComponent<MonoBlock>();
            monoBlock.Id = id;
            gameObjectMonoBlock.name = monoBlock.ToString();
            return monoBlock;
        }
    }
}