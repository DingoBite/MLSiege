using System;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace Game.Scripts.Agents.Interfaces
{
    public interface IObservationsCollector
    {
        void Init(Func<Vector3Int, Vector3Int, int?> relativeValue);
        void CollectObservations(VectorSensor sensor, Vector3Int agentCoords, Vector3Int goalCoords);
    }
}