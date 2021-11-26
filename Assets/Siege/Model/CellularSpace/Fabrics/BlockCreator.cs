using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Fabrics
{
    public class BlockCreator: IBlockCreator
    {
        private readonly IBlockFabric _blockFabric;
        private readonly IMonoBlockFabric _monoBlockFabric;
        private readonly IGridCoordsConverter _gridCoordsConverter;

        [Inject]
        public BlockCreator(
            IBlockFabric blockFabric,
            IMonoBlockFabric monoBlockFabric,
            IGridCoordsConverter gridCoordsConverter)
        {
            _blockFabric = blockFabric;
            _monoBlockFabric = monoBlockFabric;
            _gridCoordsConverter = gridCoordsConverter;
        }

        public OverallBlock Create(int id, Vector3Int coords, BlockFeatures blockFeatures)
        {
            var block = _blockFabric.MakeBlock(id, blockFeatures);
            var position = _gridCoordsConverter.Convert(coords);
            return new OverallBlock(block, _monoBlockFabric.MakeMonoBlock(coords.y, position, block), coords);
        }

        public OverallBlock Create(int id, MonoBlock block)
        {
            block.Id = id;
            block.name = block.ToString();
            var coords = _gridCoordsConverter.Convert(block.transform.position);
            return new OverallBlock(_blockFabric.MakeBlock(id, block.ScriptableObjectInfo()), block, coords);
        }
    }
}