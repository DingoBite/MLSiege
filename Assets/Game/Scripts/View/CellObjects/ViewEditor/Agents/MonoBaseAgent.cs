using System;
using Game.Scripts.CellObjects.CellObjectCharacteristics;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.PathFind;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.ViewEditor.Agents
{
    public class MonoBaseAgent : MonoAgent
    {
        private static readonly StepData ImpossibleStep = new StepData(int.MaxValue, Vector3Int.zero);
        private static readonly StepData DownStep = new StepData(0, Vector3Int.down);
        private static StepData FlagStep(Vector3Int coords) => new StepData(0, coords);
        private static StepData MoveStep(Vector3Int coords) => new StepData(1, coords);
        private static StepData BrakeUnderStep(Vector3Int coords, BlockCharacteristic blockCharacteristic, int strength) =>
            new StepData(blockCharacteristic.Durability / strength, coords);
        private static StepData BrakeStep(Vector3Int coords, BlockCharacteristic blockCharacteristic, int strength) =>
            new StepData(blockCharacteristic.Durability / strength + 1, coords);
        
        private static StepData BrakeStep(Vector3Int coords, BlockCharacteristic bc0, BlockCharacteristic bc1, int strength) =>
            new StepData((bc0.Durability + bc1.Durability) / strength + 1, coords);

        private static readonly Vector3Int[] Neighbors = {
            Vector3Int.forward,
            Vector3Int.back,
            Vector3Int.left,
            Vector3Int.right,
            Vector3Int.up + Vector3Int.forward,
            Vector3Int.up + Vector3Int.back,
            Vector3Int.up + Vector3Int.left,
            Vector3Int.up + Vector3Int.right,
            Vector3Int.down
        };

        [SerializeField] private int _strength;

        [SerializeField] private bool _isModifiable;

        private AgentCharacteristic _headCharacteristic;

        protected override bool IsModifiable => _isModifiable;

        protected override AgentCharacteristic HeadCharacteristics => _headCharacteristic;

        protected override AgentPartCharacteristic LegsCharacteristic => new AgentPartCharacteristic();

        private void Awake()
        {
            _headCharacteristic = new AgentCharacteristic(_strength, StepFunc, Neighbors);
        }

        private static bool IsNullOrEmpty(ICell cell) => cell == null || cell.IsEmpty;
        
        private StepData StepFunc(ICell cFrom, ICell cTo)
        {
            var moveVector = cTo.Coords - cFrom.Coords;
            if (moveVector == Vector3Int.down)
                return D(cFrom, cTo);
            
            ICell cToU;
            bool isNullOrEmptyCToU;
            if (moveVector.y > 0)
            {
                cTo.CellGrid.TryGetCell(cTo.Coords + Vector3Int.down, out var cToD);
                var isNullOrEmptyCToD = IsNullOrEmpty(cToD);
                if (isNullOrEmptyCToD)
                    return ImpossibleStep;

                cTo.CellGrid.TryGetCell(cTo.Coords + Vector3Int.up, out cToU);
                isNullOrEmptyCToU = IsNullOrEmpty(cToU);
                
                if (!isNullOrEmptyCToU)
                    return ImpossibleStep;

                if (cTo.IsEmpty)
                    return MoveStep(moveVector);
                
                return EN(moveVector, cFrom, cTo, cToD);
            }
            
            cTo.CellGrid.TryGetCell(cTo.Coords + Vector3Int.up, out cToU);
            isNullOrEmptyCToU = IsNullOrEmpty(cToU);
            if (cTo.IsEmpty)
            {
                if (isNullOrEmptyCToU) return EE(moveVector, cFrom, cTo, cToU);
                return EN(moveVector, cFrom, cTo, cToU);
            }
            
            if (isNullOrEmptyCToU) return NE(moveVector, cFrom, cTo, cToU);
            return NN(moveVector, cFrom, cTo, cToU);
        }

        private StepData D(ICell cFrom, ICell cTo)
        {
            if (cTo.IsEmpty) 
                return DownStep;
            switch (cTo.CellObject.CellObjectType)
            {
                case CellObjectType.Block:
                    if (cTo.CellObject.Characteristics is BlockCharacteristic cToCharacteristic)
                        return BrakeStep(Vector3Int.down, cToCharacteristic, _headCharacteristic.Strength);
                    throw new Exception("What block?");
                case CellObjectType.Agent:
                    return ImpossibleStep;
                case CellObjectType.AgentPart:
                    return ImpossibleStep;
                case CellObjectType.Flag:
                    return FlagStep(Vector3Int.down);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private StepData NN(Vector3Int moveVector, ICell cFrom, ICell cTo0, ICell cTo1)
        {
            switch (cTo0.CellObject.CellObjectType)
            {
                case CellObjectType.Block:
                    switch (cTo1.CellObject.CellObjectType)
                    {
                        case CellObjectType.Block:
                            if (cTo0.CellObject.Characteristics is BlockCharacteristic cTo0C &&
                                cTo1.CellObject.Characteristics is BlockCharacteristic cTo1C)
                                return BrakeStep(moveVector, cTo0C, cTo1C, _headCharacteristic.Strength);
                            throw new Exception("What block?");
                        case CellObjectType.Agent:
                            return ImpossibleStep;
                        case CellObjectType.AgentPart:
                            return ImpossibleStep;
                        case CellObjectType.Flag:
                            return FlagStep(moveVector);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                case CellObjectType.Agent:
                    return ImpossibleStep;
                case CellObjectType.AgentPart:
                    return ImpossibleStep;
                case CellObjectType.Flag:
                    return FlagStep(moveVector);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private StepData EN(Vector3Int moveVector, ICell cFrom, ICell cTo0, ICell cTo1)
        {
            switch (cTo1.CellObject.CellObjectType)
            {
                case CellObjectType.Block:
                    if (cTo1.CellObject.Characteristics is BlockCharacteristic cTo1C)
                        return BrakeStep(moveVector, cTo1C, _headCharacteristic.Strength);
                    throw new Exception("What block?");
                case CellObjectType.Agent:
                    return ImpossibleStep;
                case CellObjectType.AgentPart:
                    return ImpossibleStep;
                case CellObjectType.Flag:
                    return FlagStep(moveVector);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private StepData NE(Vector3Int moveVector, ICell cFrom, ICell cTo0, ICell cTo1)
        {
            switch (cTo0.CellObject.CellObjectType)
            {
                case CellObjectType.Block:
                    if (cTo0.CellObject.Characteristics is BlockCharacteristic cTo0C)
                        return BrakeStep(moveVector, cTo0C, _headCharacteristic.Strength);
                    throw new Exception("What block?");
                case CellObjectType.Agent:
                    return ImpossibleStep;
                case CellObjectType.AgentPart:
                    return ImpossibleStep;
                case CellObjectType.Flag:
                    return FlagStep(moveVector);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private StepData EE(Vector3Int moveVector, ICell cFrom, ICell cTo0, ICell cTo1) => MoveStep(moveVector);

        private void OnValidate()
        {
            if (_strength < 1)
                _strength = 1;
            else if (_strength > 100)
                _strength = 100;
        }
    }
}