using Assets.Siege.Model.General.Enums;
using Assets.Siege.MonoBehaviors.Blocks.ScriptableBlockTypes;
using UnityEngine;

namespace Assets.Siege.MonoBehaviors.Blocks.ScriptableBlocks
{
    [CreateAssetMenu(fileName = "Dirt", menuName = "ScriptableObjects/Blocks/Dirt")]
    public class Dirt: DestructibleBlocks
    {
        public override BlockInfo GetInfo() => new BlockInfo(BlockType.Dirt, BlockSolidity.Solid);
    }
}