using UnityEngine;

namespace Assets.Siege.View.Blocks.ScriptableBlockTypes
{
    public abstract class DestructibleBlock: ScriptableBlock
    {
        [Range(0, 100)] [SerializeField] private int _durability;
    }
}