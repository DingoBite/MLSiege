using UnityEngine;

namespace Assets.Siege.Model.Agents
{
    public class SiegeAgent
    {
        public readonly int Id;
        public readonly AgentFeatures Features;

        public SiegeAgent(int id, AgentInfo agentInfo)
        {
            Id = id;
            Features = new AgentFeatures(agentInfo);
        }

        public Vector3Int Coords { get; private set; }

        public bool Move(Directions direction)
        {
            return true;
        }

        public bool MoveTo(Vector3Int coords)
        {
            Coords = coords;
            return true;
        }
    }
}