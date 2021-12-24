using System;
using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.View.General.MonoBehaviors;
using Assets.Siege.View.General.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations.ScriptableBlockTypes
{
    public abstract class CommonBlock : ActableScriptableObject<BlockInfo>
    {
        public override void Move(ActableMono block, Vector3 position, Action postAnimationAction = null)
        {
            block.transform.position = position;
            postAnimationAction?.Invoke();
        }
    }
}