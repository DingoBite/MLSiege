using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Fabrics
{
    public class PackBlockFabric: IPackBlockFabric
    {
        private readonly IBlockFabric _blockFabric;
        private readonly IMonoBlockFabric _monoBlockFabric;
        private readonly IGridCoordsConverter _gridCoordsConverter;

        [Inject]
        public PackBlockFabric(
            IBlockFabric blockFabric,
            IMonoBlockFabric monoBlockFabric,
            IGridCoordsConverter gridCoordsConverter)
        {
            _blockFabric = blockFabric;
            _monoBlockFabric = monoBlockFabric;
            _gridCoordsConverter = gridCoordsConverter;
        }

        public PackBlock Make(int id, Vector3Int coords, BlockFeatures blockFeatures)
        {
            var block = _blockFabric.Make(id, blockFeatures);
            var position = _gridCoordsConverter.Convert(coords);
            return new PackBlock(block, _monoBlockFabric.Make(coords.y, position, block), coords);
        }

        public PackBlock Make(int id, MonoBlock block)
        {
            block.Id = id;
            block.name = block.ToString();
            var coords = _gridCoordsConverter.Convert(block.transform.position);
            return new PackBlock(_blockFabric.Make(id, block.ScriptableObjectInfo()), block, coords);
        }
    }
}