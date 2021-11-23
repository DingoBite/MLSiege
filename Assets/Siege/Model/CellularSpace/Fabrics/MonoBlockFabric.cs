using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.CellularSpace.Fabrics
{
    public class MonoBlockFabric: IMonoBlockFabric
    {

        public MonoBlockFabric() { }
        public MonoBlock MakeMonoBlock(int id, AbstractBlock block)
        {
            throw new System.NotImplementedException();
        }
    }
}