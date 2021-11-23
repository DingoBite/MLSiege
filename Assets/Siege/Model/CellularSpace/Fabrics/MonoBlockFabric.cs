using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Fabrics
{
    public class MonoBlockFabric: IMonoBlockFabric
    {
        public MonoBlockFabric() { }
        public MonoBlock MakeMonoBlock(int id, Vector3 position, AbstractBlock block)
        {
            var kek = GameObject.CreatePrimitive(PrimitiveType.Cube);
            kek.transform.position = position;
            return kek.AddComponent<MonoBlock>();
        }
    }
}