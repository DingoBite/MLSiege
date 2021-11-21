using System.Collections.Generic;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.General.CellularSpace.CellularSpaceGenerator
{
    internal interface ICellableGridConverter
    {
        public Dictionary<int, CellableMono> CellableMonosFromGrid(Grid gameObjectsGrid);
    }
}