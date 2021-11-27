using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IMonoBlockFabric
    {
        public MonoBlock Make(int level, Vector3 position, Block block);
    }
}