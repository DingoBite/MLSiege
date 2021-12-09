using System;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.View.Blocks.Abstracts;
using UnityEngine;

namespace Assets.Siege.View.Blocks
{
    public class MonoBlock : BlockSpaceMonoObject<BlockInfo>
    {
        public override void Move(Vector3 position, Action action = null) => this.transform.position = position;
        public override void Act<T>(T actType, Action postAnimationAction = null)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"{_scriptableObjectInfo.GetInfo().BlockData.BlockType}";
        }
    }
}
