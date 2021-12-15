using System;
using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations.ScriptableBlockTypes
{
    public abstract class DestructibleBlock: ActionCommitterBlock
    {
        [Range(0, 100)] [SerializeField] private int _durability;

        public override void Act<T>(ActableMono self, T actType, Action postAnimationAction = null)
        {
            throw new NotImplementedException();
        }

        protected override void CommitAction(FrameAgent sender, FrameBlock committer, IFrameSpaceInfo<FrameAgent> senderSpace,
            IFrameSpaceInfo<FrameBlock> committerSpace, AgentAction action)
        {
            throw new NotImplementedException();
        }

        protected override void CommitAction(FrameBlock sender, FrameBlock committer, IFrameSpaceInfo<FrameBlock> senderSpace,
            IFrameSpaceInfo<FrameBlock> committerSpace, BlockAction action)
        {
            throw new NotImplementedException();
        }
    }
}