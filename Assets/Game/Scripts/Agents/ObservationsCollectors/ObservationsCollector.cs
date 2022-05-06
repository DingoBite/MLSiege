using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Agents.Learning;
using Game.Scripts.General.StaticUtils;
using Game.Scripts.PathFind;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace Game.Scripts.Agents.Interfaces
{
    public class ObservationsCollector : IObservationsCollector
    {
        private Func<Vector3Int> _minCoordsGetter;
        private Func<Vector3Int> _maxCoordsGetter;
        private Func<Vector3Int, Vector3Int, int?> _relativeValue;

        // private Vector3Int _discreteRay;
        private Vector3Int[] _stateEnvironment;
        
        public void Init(Func<Vector3Int> minCoords, Func<Vector3Int> maxCoords, Func<Vector3Int, Vector3Int, int?> relativeValue)
        {
            _minCoordsGetter = minCoords;
            _maxCoordsGetter = maxCoords;
            _relativeValue = relativeValue;
            _stateEnvironment = Vector3IntUtils.GetPointsInRect(new Vector3Int(4, 3,4))
                .Where(c => c != Vector3Int.zero && c != Vector3Int.down)
                .ToArray();
        }

        public void CollectObservations(VectorSensor sensor, Vector3Int agentCoords)
        {
            foreach (var environmentPoint in _stateEnvironment)
            {
                var environmentCoords = environmentPoint + agentCoords;
                var value = _relativeValue.Invoke(agentCoords, environmentCoords);
                sensor.AddObservation(value ?? int.MinValue);
            }
        }

        // private bool IsInGrind(Vector3Int coords)
        // {
        //     var minCoords = _minCoordsGetter();
        //     var maxCoords = _maxCoordsGetter();
        //     return !(coords.x > maxCoords.x ||
        //              coords.x < minCoords.x ||
        //              coords.y > maxCoords.y ||
        //              coords.y < minCoords.y ||
        //              coords.z > maxCoords.z ||
        //              coords.z < minCoords.z);
        // }
    }
}