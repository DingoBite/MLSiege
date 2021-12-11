using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.Model.BlockSpace.General;

namespace Assets.Siege.Model.BlockSpace.Agents
{

    public readonly struct AgentData
    {
        public readonly AgentType AgentType;

        public AgentData(AgentType agentType)
        {
            AgentType = agentType;
        }
    }

    public class AgentInfo
    {
        public readonly AgentData AgentData;
        public readonly BlockObjectBehavior<FrameAgent, FrameAgent, AgentAction> AgentToAgentBehavior;
        public readonly BlockObjectBehavior<FrameBlock, FrameAgent, BlockAction> BlockToAgentBehavior;

        public AgentInfo(AgentData agentData, 
            BlockObjectBehavior<FrameAgent, FrameAgent, AgentAction> agentToAgentBehavior,
            BlockObjectBehavior<FrameBlock, FrameAgent, BlockAction> blockToAgentBehavior)
        {
            AgentData = agentData;
            AgentToAgentBehavior = agentToAgentBehavior;
            BlockToAgentBehavior = blockToAgentBehavior;
        }

        public AgentType AgentType => AgentData.AgentType;
    }
}