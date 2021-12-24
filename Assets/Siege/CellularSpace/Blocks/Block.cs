using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.Agents.Enums;
using Assets.Siege.CellularSpace.Blocks.Enums;
using Assets.Siege.CellularSpace.Features;
using Assets.Siege.CellularSpace.General;
using Assets.Siege.CellularSpace.General.CellObjects;
using Assets.Siege.CellularSpace.General.Interfaces;
using Assets.Siege.CellularSpace.Repositories.Interfaces;

namespace Assets.Siege.CellularSpace.Blocks
{
    public class Block: CellObject<BlockFeatures>, 
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
            IFrameSpaceInfo<FrameAgent> agentSpace, IFrameSpaceInfo<FrameBlock> committerSpace, AgentAction action) 
            => _agentToBlockBehavior(sender, committer, agentSpace, committerSpace, action);

        public void CommitAction(FrameBlock sender, FrameBlock committer,
            IFrameSpaceInfo<FrameBlock> senderSpace, IFrameSpaceInfo<FrameBlock> committerSpace, BlockAction action)
            => _blockToBlockBehavior(sender, committer, senderSpace, committerSpace, action);

        public void CommitAction(FrameBlock sender, FrameBlock committer, IFrameSpaceInfo<FrameBlock> space, BlockAction action)
            => CommitAction(sender, committer, space, space, action);
    }
}