using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.General.StaticUtils;
using UnityEngine;

namespace Game.Scripts.SiegeML.ObservationsCollectors
{
    public class ObservationsCollector : IObservationsCollector
    {
        private Func<Vector3Int, Vector3Int, int> _relativeValue;

        private Vector3Int[] _stateEnvironment;

        private Dictionary<int, bool> _nearGoalAgents;
        
        public void Init(Func<Vector3Int, Vector3Int, int> relativeValue)
        {
            _nearGoalAgents = new Dictionary<int, bool>();
            _relativeValue = relativeValue;
            _stateEnvironment = Vector3IntUtils.GetPointsInRect(new Vector3Int(5, 5,5))
                .AsParallel()
                .Select(c => c + new Vector3Int(-2, -2, -2))
                .Where(c => c != Vector3Int.zero && c != Vector3Int.down)
                .ToArray();
        }

        public void ReInit()
        {
            _nearGoalAgents.Clear();
        }
        
        public List<float> CollectObservations(int agentId, Vector3Int agentCoords, Vector3Int goalCoords)
        {
            var observations = new List<float>();
            foreach (var environmentPoint in _stateEnvironment)
            {
                var environmentCoords = environmentPoint + agentCoords;
                if (environmentCoords == goalCoords && !_nearGoalAgents.ContainsKey(agentId))
                    _nearGoalAgents.Add(agentId, true);
                var value = _relativeValue.Invoke(agentCoords, environmentCoords);
                observations.Add(value);
            }

            var targetVectorInt = goalCoords - agentCoords;
            var targetVectorXY = new Vector2(targetVectorInt.x, targetVectorInt.y);
            var targetVectorXZ = new Vector2(targetVectorInt.x, targetVectorInt.z);
            var angleXY = Vector2.Angle(Vector2.right, targetVectorXY);
            var angleXZ = Vector2.Angle(Vector2.right, targetVectorXZ);
            var angleValueXY = (float)Math.Round(angleXY, 2);
            var angleValueXZ = (float)Math.Round(angleXZ, 2);
            observations.Add(angleValueXY);
            observations.Add(angleValueXZ);
            return observations;
        }

        public bool IsNearGoal(int agentId) => _nearGoalAgents.ContainsKey(agentId);
    }
}