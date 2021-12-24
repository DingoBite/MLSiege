using System;
using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Blocks.Enums;
using Assets.Siege.View.General.MonoBehaviors;
using Assets.Siege.View.General.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations.ScriptableBlocks
{
    [CreateAssetMenu(fileName = "Null", menuName = "ScriptableObjects/Blocks/Null", order = 1)]
    public class Null: ActableScriptableObject<BlockInfo>
    {
        public override BlockInfo GetInfo() => new BlockInfo(new BlockData(
            BlockType.Null, BlockSolidity.Unobstructed), null, null);

        public override void Move(ActableMono self, Vector3 position, Action postAnimationAction = null)
            => throw new Exception("Try to move null block");
        public override void Act<T>(ActableMono self, T actType, Action postAnimationAction = null)
            => throw new Exception("Try to act at null block");
    }
}