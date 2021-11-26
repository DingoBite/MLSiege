using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Blocks.Realizations;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.Model.ObjectFeatures.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Fabrics
{
    public class BlockFabric: IBlockFabric
    {
        private readonly IBlockFeaturesFabric _blockFeaturesFabric;

        [Inject]
        public BlockFabric(IBlockFeaturesFabric blockFeaturesFabric)
        {
            _blockFeaturesFabric = blockFeaturesFabric;
        }

        public AbstractBlock MakeBlock(int id, BlockFeatures blockFeatures)
        {
            return blockFeatures.BlockType switch
            {
                BlockType.Dirt => new DirtBlock(id, blockFeatures),
                BlockType.Stone => new StoneBlock(id, blockFeatures),
                _ => new NullBlock(id)
            };
        }

        public AbstractBlock MakeBlock(int id, BlockInfo blockInfo)
        {
            return blockInfo.BlockType switch
            {
                BlockType.Dirt => new DirtBlock(id, _blockFeaturesFabric.MakeFeatures(blockInfo)),
                BlockType.Stone => new StoneBlock(id, _blockFeaturesFabric.MakeFeatures(blockInfo)),
                _ => new NullBlock(id)
            };
        }
    }
}