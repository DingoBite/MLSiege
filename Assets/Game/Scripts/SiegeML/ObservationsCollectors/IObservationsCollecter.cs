using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.SiegeML.ObservationsCollectors
{
    public interface IObservationsCollector
    {
        void Init(Func<Vector3Int, Vector3Int, int> relativeValue);
        void ReInit();
        List<float> CollectObservations(int agentId, Vector3Int agentCoords, Vector3Int goalCoords);
        bool IsNearGoal(int agentId);
    }
}