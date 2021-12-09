using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.Model.General;
using Assets.Siege.Model.General.BlockSpaceObjects;
using Assets.Siege.Model.General.Interfaces;

namespace Assets.Siege.Model.BlockSpace.Blocks
{
    public class Block: BlockSpaceObject<BlockFeatures>, 
        IActionCommitter<FrameAgent, FrameBlock, AgentAction>,
        IActionCommitter<FrameBlock, FrameBlock, BlockAction>
    {
        private readonly BlockObjectBehavior<FrameAgent, FrameBlock, AgentAction> _agentToBlockBehavior;
        private readonly BlockObjectBehavior<FrameBlock, FrameBlock, BlockAction> _blockToBlockBehavior;

        public Block(BlockInfo blockInfo): base(new BlockFeatures(blockInfo.BlockData))
        {
            _blockToBlockBehavior = blockInfo.BlockToBlockBehavior;
            _agentToBlockBehavior = blockInfo.AgentToBlockBehavior;
        }

        public void CommitAction(FrameAgent sender, FrameBlock committer,
            IFrameSpaceContext<FrameAgent> agentSpace, IFrameSpaceContext<FrameBlock> committerSpace, AgentAction action) 
            => _agentToBlockBehavior(sender, committer, agentSpace, committerSpace, action);

        public void CommitAction(FrameBlock sender, FrameBlock committer,
            IFrameSpaceContext<FrameBlock> senderSpace, IFrameSpaceContext<FrameBlock> committerSpace, BlockAction action)
            => _blockToBlockBehavior(sender, committer, senderSpace, committerSpace, action);

        public void CommitAction(FrameBlock sender, FrameBlock committer, IFrameSpaceContext<FrameBlock> space, BlockAction action)
            => CommitAction(sender, committer, space, space, action);
    }
}