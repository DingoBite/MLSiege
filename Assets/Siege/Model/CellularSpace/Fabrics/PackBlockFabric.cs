using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Converters.Interfaces;
using Assets.Siege.Model.CellularSpace.Fabrics.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Fabrics
{
    public class PackBlockFabric: IPackBlockFabric
    {
        private readonly IMonoBlockFabric _monoBlockFabric;
        private readonly IGridCoordsConverter _gridCoordsConverter;

        [Inject]
        public PackBlockFabric(
            IMonoBlockFabric monoBlockFabric,
            IGridCoordsConverter gridCoordsConverter)
        {
            _monoBlockFabric = monoBlockFabric;
            _gridCoordsConverter = gridCoordsConverter;
        }

        public PackBlock Make(int id, Vector3Int coords, BlockInfo blockInfo)
        {
            var block = new Block(id, blockInfo);
            var position = _gridCoordsConverter.Convert(coords);
            return new PackBlock(block, _monoBlockFabric.Make(coords.y, position, block), coords);
        }

        public PackBlock Make(int id, MonoBlock block)
        {
            block.Id = id;
            block.name = block.ToString();
            var coords = _gridCoordsConverter.Convert(block.transform.position);
            return new PackBlock(new Block(id, block.GetInfo()), block, coords);
        }
    }
}