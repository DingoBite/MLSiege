using Assets.Siege.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.MonoBehaviors.Blocks.ScriptableBlockTypes
{
    public abstract class DestructibleBlocks: InfoScriptableObject<BlockInfo>
    {
        [Range(0, 100)] [SerializeField] private int _durability;
    }
}