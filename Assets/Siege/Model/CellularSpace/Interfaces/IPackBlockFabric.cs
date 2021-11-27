using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IPackBlockFabric
    {
        public PackBlock Make(int id, Vector3Int coords, BlockFeatures blockFeatures);
        public PackBlock Make(int id, MonoBlock block);
    }
}