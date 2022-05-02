using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridStep;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics
{
    public class AgentCharacteristic : ICharacteristics
    {
        public int Strength { get; private set; }

        public AgentCharacteristic(int strength, Func<ICell, ICell, StepData> stepFunc,
            IEnumerable<Vector3Int> neighbors = null)
        {
            if (neighbors == null)
            {
                neighbors = new[]
                {
                    Vector3Int.forward,
                    Vector3Int.back,
                    Vector3Int.left,
                    Vector3Int.right,
                    Vector3Int.up,
                    Vector3Int.down + Vector3Int.forward,
                    Vector3Int.down + Vector3Int.back,
                    Vector3Int.down + Vector3Int.left,
                    Vector3Int.down + Vector3Int.right,
                    Vector3Int.down * 2
                };
            }
            
            Neighbors = neighbors;
            Strength = strength;
            StepFunc = stepFunc;
        }

        public IEnumerable<Vector3Int> Neighbors { get; }
        public Func<ICell, ICell, StepData> StepFunc { get; }
        public CellObjectType CellObjectType => CellObjectType.Agent;
    }
}