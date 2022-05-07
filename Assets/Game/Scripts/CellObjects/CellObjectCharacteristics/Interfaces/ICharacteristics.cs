using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridStep;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellObjects.CellObjectCharacteristics.Interfaces
{
    public interface ICharacteristics
    {
        IEnumerable<Vector3Int> Neighbors { get; }
        Func<ICell, ICell, StepData> StepFunc { get; }
        CellObjectType CellObjectType { get; }
    }
}