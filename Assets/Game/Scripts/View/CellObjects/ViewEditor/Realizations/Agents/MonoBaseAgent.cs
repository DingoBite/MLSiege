using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellObjects.Enums.Agent;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridStep;
using Game.Scripts.General.FlexibleDataApi;
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
            var stepPerformanceParam = new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.StepMove,
                vector3IntParam: c2.Coords - c1.Coords);
            if (c2.IsEmpty)
                return new StepData(1, new PerformanceParam[]{stepPerformanceParam});
            if (c2.CellObject.CellObjectType == CellObjectType.Agent)
                return new StepData(int.MaxValue, null);
            if (c2.CellObject.CellObjectType == CellObjectType.Flag)
            {
                var grabFlagPerformanceParam = new ActPerformanceParam<CellAgentAction>(CellAgentAction.Hit,
                    intParam: 1,
                    vector3IntParam: c2.Coords - c1.Coords);
                return new StepData(0, new PerformanceParam[]{ grabFlagPerformanceParam});
            }
            if (!(c2.CellObject.Characteristics is BlockCharacteristic blockCharacteristics))
                throw new NullReferenceException();
            var cost = blockCharacteristics.Durability / _headCharacteristic.Strength;
            var pathMovementParams = new List<PerformanceParam>(cost);
            for (var i = 0; i < cost; i++)
            {
                pathMovementParams[i] = new ActPerformanceParam<CellAgentAction>(CellAgentAction.Hit,
                    intParam: _headCharacteristic.Strength,
                    vector3IntParam: c2.Coords - c1.Coords);
            }
            pathMovementParams.Add(stepPerformanceParam);
            return new StepData(cost + 1, pathMovementParams);
        }
    }
}