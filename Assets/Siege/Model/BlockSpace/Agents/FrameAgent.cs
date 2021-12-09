using System;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.Model.General.BlockSpaceObjects;
using Assets.Siege.View.Agents;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Agents
{
    public class FrameAgent : BlockObjectFrame<FrameAgent, Agent, MonoAgent, AgentFeatures>
    {
        public FrameAgent(IFrameSpaceContext<FrameAgent> frameSpace, Agent data, MonoAgent mono, Vector3Int coords) : base(frameSpace, data, mono, coords)
        {
        }

        public override AgentFeatures Features => _data.Features;

        private void PostAnimationCommit(Action commitAction)
        {
            // TODO Animation, postAnimation commit
            _mono.transform.localScale *= 0.8f;
            commitAction?.Invoke();
        }


        public void CommitAction(FrameAgent sender, AgentAction action, IFrameSpaceContext<FrameAgent> senderSpace)
            => PostAnimationCommit(() => _data.CommitAction(sender, this, senderSpace, _frameSpace, action));

        public void CommitAction(FrameBlock sender, BlockAction action, IFrameSpaceContext<FrameBlock> senderSpace)
            => PostAnimationCommit(() => _data.CommitAction(sender, this, senderSpace, _frameSpace, action));

        public void CommitAction(FrameAgent sender, AgentAction action)
            => PostAnimationCommit(() => _data.CommitAction(sender, this, _frameSpace, action));
    }
}