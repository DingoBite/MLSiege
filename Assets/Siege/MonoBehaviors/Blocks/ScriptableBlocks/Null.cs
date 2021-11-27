using Assets.Siege.Model.General.Enums;
using Assets.Siege.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.MonoBehaviors.Blocks.ScriptableBlocks
{
    [CreateAssetMenu(fileName = "Null", menuName = "ScriptableObjects/Blocks/Null", order = 1)]
    public class Null: InfoScriptableObject<BlockInfo>
    {
        public override BlockInfo GetInfo() => new BlockInfo(BlockType.Null, BlockSolidity.Unobstructed);
    }
}