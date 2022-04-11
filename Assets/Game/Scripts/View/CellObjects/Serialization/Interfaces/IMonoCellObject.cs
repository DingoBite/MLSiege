using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using UnityEngine;

namespace Game.Scripts.View.CellObjects.Serialization.Interfaces
{
    public interface IMonoCellObject
    {
        Vector3 MainPosition { get; }
    }
}