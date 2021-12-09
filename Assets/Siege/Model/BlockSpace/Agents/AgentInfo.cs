using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;

namespace Assets.Siege.Model.BlockSpace.Agents
{
    public delegate bool AgentBehavior(int sender, int committer, IFrameSpaceContext<FrameBlock> frameSpace, AgentAction blockAction);

    public readonly struct AgentData
    {
        
    }

    public class AgentInfo
    {
        public readonly AgentData AgentData;
        public readonly AgentBehavior AgentBehavior;

        public AgentInfo(AgentData agentData, AgentBehavior agentBehavior)
        {
            AgentData = agentData;
            AgentBehavior = agentBehavior;
        }
    }
}