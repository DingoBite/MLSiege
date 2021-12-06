using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.View.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Fabrics.Interfaces
{
    public interface IMonoBlockFabric
    {
        public MonoBlock Make(int level, Vector3 position, Block block);
    }
}