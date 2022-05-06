using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Agents.Interfaces;
using Game.Scripts.Agents.Learning;
using Game.Scripts.CellularSpace;
using Game.Scripts.General;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace Game.Scripts.Agents
{
    public class AgentManager : IAgentManager
    {
        private IGridFacade _gridFacade;
        private IObservationsCollector _observationsCollector;
        private IActionResolver _actionResolver;
        
        private List<int> _agentIds;
        private RoundingInt _roundingAgentIndex;

        private Action _winAction;
        private Action _loseAction;
        
        public void Init(IGridFacade gridFacade, IObservationsCollector observationsCollector, IActionResolver actionResolver,
            Action winAction, Action loseAction)
        {
            _gridFacade = gridFacade;
            _observationsCollector = observationsCollector;
            _observationsCollector.Init(
                () => _gridFacade.MinCoords, () => _gridFacade.MaxCoords, _gridFacade.GetRelativeValue);
            _actionResolver = actionResolver;
            _agentIds = _gridFacade.GetAgentIds().ToList();
            _roundingAgentIndex = new RoundingInt(_agentIds.Count);
            _winAction = winAction;
            _loseAction = loseAction;
        }

        private int CurrentAgentId => _agentIds[_roundingAgentIndex.CurrentIndex];

        public void CollectObservations(VectorSensor sensor)
        {
            if (!_gridFacade.TryGetCoordsFromId(CurrentAgentId, out var coords)) return;
            _observationsCollector.CollectObservations(sensor, coords);
        }

        public void ResolveAction(ActionBuffers actions, Agent agent)
        {
            _gridFacade.ApplyGlobalAction();
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