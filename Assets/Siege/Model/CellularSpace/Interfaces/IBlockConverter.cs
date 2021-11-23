using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockConverter
    {
        public OverallBlock Convert(Vector3Int coords, AbstractBlock block, int id);
        public OverallBlock Convert(MonoBlock block, int id);
    }
}