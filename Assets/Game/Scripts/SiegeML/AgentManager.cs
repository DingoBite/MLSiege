using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace;
using Game.Scripts.General;
using Game.Scripts.SiegeML.ActionResolvers;
using Game.Scripts.SiegeML.ObservationsCollectors;
using Unity.MLAgents.Actuators;
using UnityEngine;

namespace Game.Scripts.SiegeML
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

        private bool _isWinHit;
        
        public void Init(IGridFacade gridFacade, IObservationsCollector observationsCollector, IActionResolver actionResolver,
            int agentsNearGoalToWin,
            Action winAction, Action loseAction)
        {
            _gridFacade = gridFacade;
            _observationsCollector = observationsCollector;
            _observationsCollector.Init(
                (senderCoords, targetCoords) => _gridFacade.GetRelativeValue(senderCoords, targetCoords));
            _actionResolver = actionResolver;
            _goalsIds = _gridFacade.GetGoalIds().ToList();
            _firstGoalId = _goalsIds[0];
            _agentIds = _gridFacade.GetAgentIds().ToList();
            _actionResolver.Init(_goalsIds, agentsNearGoalToWin, _agentIds.Count, winAction, loseAction);
            _roundingAgentIndex = new RoundingInt(_agentIds.Count);
        }

        public void SetLearnEpisodesInfo(Func<int> completeEpisodesGetter, int maxEpisodes) =>
            _actionResolver.SetLearnEpisodesInfo(completeEpisodesGetter, maxEpisodes);

        public void ReInit()
        {
            _isWinHit = false;
            _agentIds = _gridFacade.GetAgentIds().ToList();
            _goalsIds = _gridFacade.GetGoalIds().ToList();
            _observationsCollector.ReInit();
            _roundingAgentIndex = new RoundingInt(_agentIds.Count);
        }

        private int CurrentAgentId => _agentIds[_roundingAgentIndex.CurrentIndex];

        public List<float> CollectObservations()
        {
            var currentAgentId = CurrentAgentId;
            if (!_gridFacade.TryGetCoordsFromId(currentAgentId, out var agentCoords)) 
                throw new ArgumentException(nameof(currentAgentId));
            if (!_gridFacade.TryGetCoordsFromId(_firstGoalId, out var goalCoords)) 
                throw new ArgumentException(nameof(_firstGoalId));
            var observations = _observationsCollector.CollectObservations(currentAgentId, agentCoords, goalCoords);
            return observations;
        }

        public bool IsWinState() => _isWinHit && _actionResolver.IsEnoughAgentsNearGoal();

        public float ResolveAction(ActionBuffers actions)
        {
            if (_agentIds.Count == 0) return 0;
            
            var currentAgentId = CurrentAgentId;
            if (!_gridFacade.TryGetCoordsFromId(currentAgentId, out var agentCoords)) 
                throw new ArgumentException(nameof(currentAgentId));
            if (!_gridFacade.TryGetCoordsFromId(_firstGoalId, out var goalCoords)) 
                throw new ArgumentException(nameof(_firstGoalId));
            if (_observationsCollector.IsNearGoal(currentAgentId))
                _actionResolver.AddAgentNearGoal(currentAgentId);
            if (CheckWinGoalDirection(goalCoords - agentCoords))
            {
                _isWinHit = true;
                _agentIds.Remove(currentAgentId);
                _roundingAgentIndex = new RoundingInt(_roundingAgentIndex.CurrentIndex - 1, _agentIds.Count);
                return 0;
            }
            var reward = _actionResolver.ResolveAction(actions, CurrentAgentId, _gridFacade);
            _roundingAgentIndex.MoveNext();
            return reward;
        }

        public float ResolveWin() => _actionResolver.ResolveWin();

        public float ResolveLose() => _actionResolver.ResolveLose();

        private bool CheckWinGoalDirection(Vector3Int coords) =>
            coords == Vector3Int.forward ||
            coords == Vector3Int.left ||
            coords == Vector3Int.right ||
            coords == Vector3Int.back ||
            coords == Vector3Int.down * 2 ||
            coords == Vector3Int.up ||
            coords == Vector3Int.down + Vector3Int.forward ||
            coords == Vector3Int.down + Vector3Int.left ||
            coords == Vector3Int.down + Vector3Int.right ||
            coords == Vector3Int.down + Vector3Int.back;
    }
}