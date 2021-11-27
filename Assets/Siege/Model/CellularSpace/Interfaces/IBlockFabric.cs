using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.MonoBehaviors.Blocks;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockFabric
    {
        public Block Make(int id, BlockInfo blockInfo);
    }
}