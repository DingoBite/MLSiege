using System;
using System.Collections.Generic;
using Game.Scripts.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.PathFind;
using UnityEngine;

namespace Game.Scripts.CellObjects.CellObjectCharacteristics
{
    public class BlockCharacteristic : ICharacteristics
    {
        private int _durability;

        public BlockCharacteristic(int durability, int maxDurability)
        {
            IsCorrect = true;
            MaxDurability = maxDurability;
            Durability = durability;
        }

        public bool IsCorrect { get; private set; }

        public int MaxDurability { get; private set; }

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

        public void DurabilityChange(int changeValue) => Durability += changeValue;

        public IEnumerable<Vector3Int> Neighbors => null;
        public Func<ICell, ICell, StepData> StepFunc => null;
        public CellObjectType CellObjectType => CellObjectType.Block;
    }
}