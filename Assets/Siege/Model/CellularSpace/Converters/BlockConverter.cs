using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Converters
{
    public class BlockConverter: IBlockConverter
    {
        private readonly IBlockFabric _blockFabric;
        private readonly IMonoBlockFabric _monoBlockFabric;
        private readonly IGridCoordsConverter _gridCoordsConverter;

        [Inject]
        public BlockConverter(IBlockFabric blockFabric, IMonoBlockFabric monoBlockFabric, IGridCoordsConverter gridCoordsConverter)
        {
            _blockFabric = blockFabric;
            _monoBlockFabric = monoBlockFabric;
            _gridCoordsConverter = gridCoordsConverter;
        }

        public OverallBlock Convert(Vector3Int coords, AbstractBlock block, int id)
        {
            var position = _gridCoordsConverter.Convert(coords);
            return new OverallBlock(block, _monoBlockFabric.MakeMonoBlock(id, position, block), coords);
        }

        public OverallBlock Convert(MonoBlock block, int id)
        {
            var coords = _gridCoordsConverter.Convert(block.transform.position);
            return new OverallBlock(_blockFabric.MakeBlock(id, block.ScriptableObjectInfo()), block, coords);
        }
    }
}