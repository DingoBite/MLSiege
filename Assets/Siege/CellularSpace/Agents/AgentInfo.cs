using Assets.Siege.CellularSpace.Agents.Enums;
using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Blocks.Enums;
using Assets.Siege.CellularSpace.General;

namespace Assets.Siege.CellularSpace.Agents
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