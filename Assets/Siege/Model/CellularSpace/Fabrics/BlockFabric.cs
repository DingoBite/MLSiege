using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Blocks.Realizations;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
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

        public AbstractBlock MakeBlock(int id, BlockInfo blockInfo)
        {
            switch (blockInfo.BlockType)
            {
                case BlockType.Dirt:
                    return new DirtBlock(id, _blockFeaturesFabric.MakeFeatures(blockInfo));
                case BlockType.Stone:
                    return new StoneBlock(id, _blockFeaturesFabric.MakeFeatures(blockInfo));
                default:
                    return new NullBlock(id);
            }
        }
    }
}