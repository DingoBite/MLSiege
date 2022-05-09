using System;
using System.Collections.Generic;
using Game.Scripts.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.PathFind;
using UnityEngine;

namespace Game.Scripts.CellObjects.CellObjectCharacteristics
{
    public class AgentCharacteristic : ICharacteristics
    {
        public int Strength { get; private set; }

        public AgentCharacteristic(int strength, Func<ICell, ICell, StepData> stepFunc,
            IEnumerable<Vector3Int> neighbors)
        {
            Neighbors = neighbors;
            Strength = strength;
            StepFunc = stepFunc;
        }

        public IEnumerable<Vector3Int> Neighbors { get; }
        public Func<ICell, ICell, StepData> StepFunc { get; }
        public CellObjectType CellObjectType => CellObjectType.Agent;
    }
}