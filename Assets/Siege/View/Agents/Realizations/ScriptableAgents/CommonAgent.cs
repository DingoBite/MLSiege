using System;
using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using Assets.Siege.View.General.ScriptableObjects;
using UnityEngine;

namespace Assets.Siege.View.Agents.Realizations.ScriptableAgents
{
    [CreateAssetMenu(fileName = "Dirt", menuName = "ScriptableObjects/Agents/CommonAgent")]
    public class CommonAgent : ActableScriptableObject<AgentInfo>
    {
        public override AgentInfo GetInfo() =>
            new AgentInfo(new AgentData(AgentType.Agent), CommitAction, CommitAction);

        private void CommitAction(FrameBlock sender, FrameAgent committer,
            IFrameSpaceInfo<FrameBlock> senderSpace, IFrameSpaceInfo<FrameAgent> committerSpace,
            BlockAction action)
        {
            throw new NotImplementedException();
        }

        private void CommitAction(FrameAgent sender, FrameAgent committer,
            IFrameSpaceInfo<FrameAgent> senderSpace, IFrameSpaceInfo<FrameAgent> committerSpace,
            AgentAction action)
        {
            throw new NotImplementedException();
        }

        public override void Move(ActableMono agent, Vector3 position, Action postAnimationAction = null)
        {
            agent.transform.position = position;
            postAnimationAction?.Invoke();
        }

        public override void Act<T>(ActableMono agent, T actType, Action postAnimationAction = null)
        {
            throw new NotImplementedException();
        }
    }
}