using System;
using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.General.CellObjects;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Blocks
{
    public class FrameBlock: CellObjectFrame<FrameBlock, Block, MonoBlock, BlockFeatures>
    {
        public FrameBlock(IFrameSpaceContext<FrameBlock> frameSpace, Block block, MonoBlock monoBlock, Vector3Int coords)
            : base(frameSpace, block, monoBlock, coords)
        {
        }

        public override BlockFeatures Features => _data.Features;

        private void PostAnimationCommit<T>(T action, Action commitAction) where T: Enum => _mono.Act(action, commitAction);

        public void CommitAction(FrameAgent sender, AgentAction action, IFrameSpaceContext<FrameAgent> senderSpace) 
            => PostAnimationCommit(action, () => _data.CommitAction(sender, this, senderSpace, _frameSpace, action));

        public void CommitAction(FrameBlock sender, BlockAction action, IFrameSpaceContext<FrameBlock> senderSpace) 
            => PostAnimationCommit(action, () => _data.CommitAction(sender, this, senderSpace, _frameSpace, action));

        public void CommitAction(FrameBlock sender, BlockAction action) 
            => PostAnimationCommit(action, () => _data.CommitAction(sender, this, _frameSpace, action));
    }
}