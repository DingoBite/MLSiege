using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridStep;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Realizations.Agents
{
    public class MonoBaseAgent : MonoAgent
    {
        [SerializeField] private bool _isModifiable;
        private AgentCharacteristic _headCharacteristic;
        
        protected override bool IsModifiable => _isModifiable;
        protected override AgentCharacteristic HeadCharacteristics => _headCharacteristic;
        protected override AgentPartCharacteristic LegsCharacteristic => new AgentPartCharacteristic();

        private void Awake()
        {
            _headCharacteristic = new AgentCharacteristic(5, StepFunc);
        }

        private StepData StepFunc(ICell c1, ICell c2)
        {
            var moveVector = c2.Coords - c1.Coords;
            return new StepData(1, moveVector);
        }
    }
}