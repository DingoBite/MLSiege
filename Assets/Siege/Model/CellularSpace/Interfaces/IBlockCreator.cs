using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IBlockCreator
    {
        public OverallBlock Create(int id, Vector3Int coords, BlockFeatures blockFeatures);
        public OverallBlock Create(int id, MonoBlock block);
    }
}