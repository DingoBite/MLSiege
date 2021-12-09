using System;
using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.View.Blocks.Abstracts;
using UnityEngine;

namespace Assets.Siege.View.Agents
{
    public class MonoAgent: BlockSpaceMonoObject<AgentInfo>
    {
        public override void Move(Vector3 position, Action postAnimationAction = null) => this.transform.position = position;

        public override void Act<T>(T actType, Action postAnimationAction = null)
        {
            throw new NotImplementedException();
        }
    }
}