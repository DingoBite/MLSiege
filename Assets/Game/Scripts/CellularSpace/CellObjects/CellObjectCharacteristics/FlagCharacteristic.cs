using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridStep;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics
{
    public class FlagCharacteristic : ICharacteristics
    {
        public IEnumerable<Vector3Int> Neighbors => null;
        public Func<ICell, ICell, StepData> StepFunc => null;
        public CellObjectType CellObjectType => CellObjectType.Flag;
    }
}