using System;
using Game.Scripts.Agents.Learning;
using Game.Scripts.CellularSpace;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.PathFind;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace Game.Scripts.Agents.Interfaces
{
    public interface IObservationsCollector
    {
        void Init(Func<Vector3Int> minCoords, Func<Vector3Int> maxCoords,
            Func<Vector3Int, Vector3Int, int?> relativeValue);
        void CollectObservations(VectorSensor sensor, Vector3Int agentCoords);
    }
}