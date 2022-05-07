using System;
using System.Linq;
using Game.Scripts.General.StaticUtils;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace Game.Scripts.Agents.Interfaces
{
    public class ObservationsCollector : IObservationsCollector
    {
        private Func<Vector3Int, Vector3Int, int?> _relativeValue;

        // private Vector3Int _discreteRay;
        private Vector3Int[] _stateEnvironment;
        
        public void Init(Func<Vector3Int, Vector3Int, int?> relativeValue)
        {
            _relativeValue = relativeValue;
            _stateEnvironment = Vector3IntUtils.GetPointsInRect(new Vector3Int(4, 4,4))
                .AsParallel()
                .Select(c => c + new Vector3Int(-1, -1, -1))
                .Where(c => c != Vector3Int.zero && c != Vector3Int.down)
                .ToArray();
        }

        public void CollectObservations(VectorSensor sensor, Vector3Int agentCoords, Vector3Int goalCoords)
        {
            foreach (var environmentPoint in _stateEnvironment)
            {
                var environmentCoords = environmentPoint + agentCoords;
                var value = _relativeValue.Invoke(agentCoords, environmentCoords);
                sensor.AddObservation(value ?? int.MinValue);
            }

            var targetVectorInt = goalCoords - agentCoords;
            var targetVector = new Vector3(targetVectorInt.x, 0, targetVectorInt.z);
            var angle = Vector3.Angle(Vector3.right, targetVector);
            var angleValue = (float)Math.Round(angle, 4);
            sensor.AddObservation(angleValue);
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