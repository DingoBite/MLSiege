using System;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridStep;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Realizations.Agents
{
    public class MonoBaseAgent : MonoAgent
    {
        [SerializeField] private bool _isModifiable;
        protected override bool IsModifiable => _isModifiable;
        protected override AgentCharacteristic HeadCharacteristics => _headCharacteristic;

        private AgentCharacteristic _headCharacteristic;

        protected override void OnStart()
        {
            _headCharacteristic = new AgentCharacteristic(5, StepFunc);
        }

        private StepData StepFunc(ICell c1, ICell c2)
        {
            if (c2.IsEmpty) return new StepData(c1, c2, 1);
            if (c2.CellObject.CellObjectType == CellObjectType.Agent) return new StepData(c1, c2);
            if (!(c2.CellObject.Characteristics is BlockCharacteristic blockCharacteristics))
                throw new NullReferenceException();
            var cost = blockCharacteristics.Durability / _headCharacteristic.Strength;
            return new StepData(c1, c2, cost + 1);
        }
    }
}