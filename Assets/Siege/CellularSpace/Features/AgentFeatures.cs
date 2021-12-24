using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.Agents.Enums;

namespace Assets.Siege.CellularSpace.Features
{
    public class AgentFeatures : AbstractFeatures
    {
        public readonly AgentType AgentType;

        public AgentFeatures(AgentData agentData)
        {
            AgentType = agentData.AgentType;
        }

        public override int this[int i] => _featuresValues[i];
    }
}