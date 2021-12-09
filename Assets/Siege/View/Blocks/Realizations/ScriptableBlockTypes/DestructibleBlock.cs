using Assets.Siege.View.Blocks.Abstracts;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations.ScriptableBlockTypes
{
    public abstract class DestructibleBlock: ScriptableBlock
    {
        [Range(0, 100)] [SerializeField] private int _durability;
    }
}