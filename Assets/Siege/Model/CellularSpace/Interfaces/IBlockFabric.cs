using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockFabric
    {
        public CommonBlock Make(int id, BlockFeatures blockFeatures);
        public CommonBlock Make(int id, BlockInfo blockInfo);
    }
}