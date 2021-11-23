using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockFabric
    {
        public AbstractBlock MakeBlock(int id, BlockInfo blockInfo);
    }
}