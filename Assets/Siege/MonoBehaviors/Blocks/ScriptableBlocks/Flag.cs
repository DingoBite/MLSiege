using Assets.Siege.Model.General.Enums;
using Assets.Siege.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.MonoBehaviors.Blocks.ScriptableBlocks
{
    [CreateAssetMenu(fileName = "Flag", menuName = "ScriptableObjects/Blocks/Flag")]
    public class Flag: InfoScriptableObject<BlockInfo>
    {
        public override BlockInfo GetInfo() => new BlockInfo(BlockType.Flag, BlockSolidity.Unobstructed);
    }
}