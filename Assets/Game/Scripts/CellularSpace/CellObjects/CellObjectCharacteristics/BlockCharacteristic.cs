using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridStep;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics
{
    public class BlockCharacteristic : ICharacteristics
    {
        public bool IsCorrect { get; private set; }

        public int Durability
        {
            get => _durability;
            private set
            {
                if (!IsCorrect) return;
                if (value <= 0) IsCorrect = false;
                else if (value >= MaxDurability) _durability = MaxDurability;
                else _durability = value;
            }
        }

        private int _durability;
        
        public int MaxDurability { get; private set; }

        public BlockCharacteristic(int durability, int maxDurability)
        {
            Durability = durability;
            MaxDurability = maxDurability;
        }

        public void DurabilityChange(int changeValue)
        {
            Durability += changeValue;
        }
        
        public IEnumerable<Vector3Int> Neighbors => null;
        public Func<ICell, ICell, StepData> StepFunc => null;
        public CellObjectType CellObjectType => CellObjectType.Block;
    }
}