using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.Blocks;

namespace Assets.Siege.Model.CellularSpace.Fabrics
{
    public class BlockFabric: IBlockFabric
    {
        public Block Make(int id, BlockInfo blockInfo) => new Block(id, new BlockFeatures(blockInfo));
    }
}