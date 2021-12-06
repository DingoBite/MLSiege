using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.View.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Fabrics.Interfaces
{
    public interface IPackBlockFabric
    {
        public PackBlock Make(int id, Vector3Int coords, BlockInfo blockInfo);
        public PackBlock Make(int id, MonoBlock block);
    }
}