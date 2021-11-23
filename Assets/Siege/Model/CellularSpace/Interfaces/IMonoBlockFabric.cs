using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IMonoBlockFabric
    {
        public MonoBlock MakeMonoBlock(int id, AbstractBlock block);
    }
}