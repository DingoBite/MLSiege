using Assets.Siege.Model.ObjectFeatures.Blocks.Realizations;
using Assets.Siege.Model.ObjectFeatures.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.ObjectFeatures.Blocks.Fabrics
{
    public class BlockFeaturesFabric: IBlockFeaturesFabric
    {
        public BlockFeaturesFabric() { }

        public BlockFeatures MakeFeatures(BlockInfo blockInfo) => new NullBlockFeatures();
    }
}