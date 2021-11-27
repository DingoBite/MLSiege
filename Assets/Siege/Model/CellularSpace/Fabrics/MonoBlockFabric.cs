using System.Collections.Generic;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.MonoBehaviors.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Fabrics
{
    public class MonoBlockFabric: IMonoBlockFabric
    {
        private readonly IDictionary<BlockType, MonoBlock> _blockPrefabs;
        private readonly IGameObjectGrid _parentGameObjectGrid;

        [Inject]
        public MonoBlockFabric(IDictionary<BlockType, MonoBlock> blockPrefabs, IGameObjectGrid gameObjectGrid)
        {
            _blockPrefabs = blockPrefabs;
            _parentGameObjectGrid = gameObjectGrid;
        }

        public MonoBlock Make(int level, Vector3 position, Block block)
        {
            var monoBlockPrefab = _blockPrefabs[block.Features.BlockType];
            var gameObjectMonoBlock = Object.Instantiate(monoBlockPrefab, position, Quaternion.identity, _parentGameObjectGrid.GetLevel(level).transform);
            var monoBlock = gameObjectMonoBlock.GetComponent<MonoBlock>();
            monoBlock.Id = block.Id;
            gameObjectMonoBlock.name = monoBlock.ToString();
            return monoBlock;
        }
    }
}