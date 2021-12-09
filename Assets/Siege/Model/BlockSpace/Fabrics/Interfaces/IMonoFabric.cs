using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.View.Blocks.Abstracts;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Fabrics.Interfaces
{
    public interface IMonoFabric<out TMono> where TMono : OperationalMono
    {
        public TMono Make(int id, int level, Vector3 position, Block block);
    }
}