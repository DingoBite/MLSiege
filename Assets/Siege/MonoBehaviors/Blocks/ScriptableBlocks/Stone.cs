using Assets.Siege.Model.General.Enums;
using Assets.Siege.MonoBehaviors.Blocks.ScriptableBlockTypes;
using UnityEngine;

namespace Assets.Siege.MonoBehaviors.Blocks.ScriptableBlocks
{
    [CreateAssetMenu(fileName = "Stone", menuName = "ScriptableObjects/Blocks/Stone")]
    public class Stone: DestructibleBlocks
    {
        public override BlockInfo GetInfo() => new BlockInfo(BlockType.Stone, BlockSolidity.Solid);
    }
}