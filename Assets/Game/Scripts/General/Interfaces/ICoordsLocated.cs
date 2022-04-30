using UnityEngine;

namespace Game.Scripts.General.Interfaces
{
    public interface ICoordsLocated
    {
        Vector3Int Coords { get; }
    }
}