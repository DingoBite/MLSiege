using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.General;
using Assets.Siege.Model.BlockSpace.General.CellObjects;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;

namespace Assets.Siege.Model.BlockSpace.Agents
{
    public class Agent : CellObject<AgentFeatures>,
        IActionCommitter<FrameAgent, FrameAgent, AgentAction>,
        IActionCommitter<FrameBlock, FrameAgent, BlockAction>
    {
        private readonly BlockObjectBehavior<FrameAgent, FrameAgent, AgentAction> _agentToAgentBehavior;
        private readonly BlockObjectBehavior<FrameBlock, FrameAgent, BlockAction> _blockToAgentBehavior;

        public Agent(AgentInfo info) : base(new AgentFeatures(info.AgentData))
        {
            _agentToAgentBehavior = info.AgentToAgentBehavior;
            _blockToAgentBehavior = info.BlockToAgentBehavior;
        }

        public void CommitAction(FrameAgent sender, FrameAgent committer, IFrameSpaceInfo<FrameAgent> senderSpace,
            IFrameSpaceInfo<FrameAgent> committerSpace, AgentAction action)
            => _agentToAgentBehavior(sender, committer, senderSpace, committerSpace, action);

        public void CommitAction(FrameBlock sender, FrameAgent committer, IFrameSpaceInfo<FrameBlock> senderSpace,
            IFrameSpaceInfo<FrameAgent> committerSpace, BlockAction action)
            => _blockToAgentBehavior(sender, committer, senderSpace, committerSpace, action);

        public void CommitAction(FrameAgent sender, FrameAgent committer, IFrameSpaceInfo<FrameAgent> space, AgentAction action)
            => CommitAction(sender, committer, space, space, action);
    }
}