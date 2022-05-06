using Game.Scripts.CellularSpace;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;

namespace Game.Scripts.Agents.Interfaces
{
    public interface IActionResolver
    {
        void Init(int goalId);
        void ResolveAction(ActionBuffers actions, int agentId, IGridFacade gridFacade, Agent mlAgent);
        float WinReward { get; }
        float LoseReward { get; }
    }
}