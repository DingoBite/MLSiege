using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.MonoBehaviors.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IPackBlockFabric
    {
        public PackBlock Make(int id, Vector3Int coords, BlockInfo blockInfo);
        public PackBlock Make(int id, MonoBlock block);
    }
}