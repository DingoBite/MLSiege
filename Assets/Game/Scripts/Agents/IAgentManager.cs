using System;
using Game.Scripts.CellularSpace;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

namespace Game.Scripts.Agents.Interfaces
{
    public interface IAgentManager
    {
        void Init(IGridFacade gridFacade, IObservationsCollector observationsCollector, IActionResolver actionResolver,
            Action winAction, Action loseAction);
        void ReInit();
        void CollectObservations(VectorSensor sensor);
        void ResolveAction(ActionBuffers actions, Agent agent);
        void ResolveWin(Agent agent);
        void ResolveLose(Agent agent);
    }
}