using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Agents.Interfaces;
using Game.Scripts.CellularSpace;
using Game.Scripts.General;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

namespace Game.Scripts.Agents
{
    public class AgentManager : IAgentManager
    {
        private IGridFacade _gridFacade;
        private IObservationsCollector _observationsCollector;
        private IActionResolver _actionResolver;
        
        private List<int> _agentIds;
        private List<int> _goalsIds;
        private RoundingInt _roundingAgentIndex;

        private int _firstGoalId;

        private Action _winAction;
        private Action _loseAction;
        
        public void Init(IGridFacade gridFacade, IObservationsCollector observationsCollector, IActionResolver actionResolver,
            Action winAction, Action loseAction)
        {
            _gridFacade = gridFacade;
            _observationsCollector = observationsCollector;
            _observationsCollector.Init(_gridFacade.GetRelativeValue);
            _actionResolver = actionResolver;
            _goalsIds = _gridFacade.GetGoalIds().ToList();
            _actionResolver.Init(_goalsIds);
            _agentIds = _gridFacade.GetAgentIds().ToList();
            _roundingAgentIndex = new RoundingInt(_agentIds.Count);
            _winAction = winAction;
            _loseAction = loseAction;
        }

        public void ReInit()
        {
            _agentIds = _gridFacade.GetAgentIds().ToList();
            _goalsIds = _gridFacade.GetGoalIds().ToList();
            _roundingAgentIndex = new RoundingInt(_agentIds.Count);
        }

        private int CurrentAgentId => _agentIds[_roundingAgentIndex.CurrentIndex];

        public void CollectObservations(VectorSensor sensor)
        {
            if (!_gridFacade.TryGetCoordsFromId(CurrentAgentId, out var agentCoords)) 
                throw new ArgumentException(nameof(CurrentAgentId));
            if (!_gridFacade.TryGetCoordsFromId(_firstGoalId, out var goalCoords)) 
                throw new ArgumentException(nameof(_firstGoalId));
            _observationsCollector.CollectObservations(sensor, agentCoords, goalCoords);
        }

        public void ResolveAction(ActionBuffers actions, Agent agent)
        {
            _actionResolver.ResolveAction(actions, CurrentAgentId, _gridFacade, agent);
            _roundingAgentIndex.MoveNext();
        }

        public void ResolveWin(Agent agent)
        {
            agent.AddReward(_actionResolver.WinReward);
            agent.EndEpisode();
            _winAction.Invoke();
        }

        public void ResolveLose(Agent agent)
        {
            agent.AddReward(_actionResolver.LoseReward);
            agent.EndEpisode();
            _loseAction.Invoke();
        }
    }
}