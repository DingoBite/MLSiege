using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.ObjectFeatures.Interfaces
{
    public interface IBlockFeaturesFabric
    {
        public BlockFeatures Make(BlockInfo blockInfo);
    }
}