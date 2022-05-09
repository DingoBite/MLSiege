using System;
using System.Collections.Generic;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.PathFind;
using UnityEngine;

namespace Game.Scripts.CellObjects.CellObjectCharacteristics.Interfaces
{
    public interface ICharacteristics
    {
        IEnumerable<Vector3Int> Neighbors { get; }
        Func<ICell, ICell, StepData> StepFunc { get; }
        CellObjectType CellObjectType { get; }
    }
}