using System;
using System.Collections.Generic;
using Game.Scripts.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.PathFind;
using UnityEngine;

namespace Game.Scripts.CellObjects.CellObjectCharacteristics
{
    public class FlagCharacteristic : ICharacteristics
    {
        public IEnumerable<Vector3Int> Neighbors => null;
        public Func<ICell, ICell, StepData> StepFunc => null;
        public CellObjectType CellObjectType => CellObjectType.Flag;
    }
}