using Assets.Siege.CellularSpace.Agents.Enums;
using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Blocks.Enums;
using Assets.Siege.CellularSpace.Features;
using Assets.Siege.CellularSpace.General;
using Assets.Siege.CellularSpace.General.CellObjects;
using Assets.Siege.CellularSpace.General.Interfaces;
using Assets.Siege.CellularSpace.Repositories.Interfaces;

namespace Assets.Siege.CellularSpace.Agents
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