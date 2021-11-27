using Assets.Siege.Model.ObjectFeatures.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.ObjectFeatures.Blocks.Fabrics
{
    public class BlockFeaturesFabric: IBlockFeaturesFabric
    {
        public BlockFeaturesFabric() { }

        public BlockFeatures Make(BlockInfo blockInfo)
        {
            return new BlockFeatures(blockInfo);
        }
    }
}