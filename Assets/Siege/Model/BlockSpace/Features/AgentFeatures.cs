using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;

namespace Assets.Siege.Model.BlockSpace.Features
{
    public class AgentFeatures : AbstractFeatures
    {
        public readonly AgentType AgentType;

        public AgentFeatures(AgentData agentData)
        {
            AgentType = agentData.AgentType;
        }
    }
}