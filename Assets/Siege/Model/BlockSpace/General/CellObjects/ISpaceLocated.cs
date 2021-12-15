using Assets.Siege.Model.BlockSpace.Features;
using Assets.Siege.Model.BlockSpace.General.CellObjects;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.General.Interfaces
{
    public interface ISpaceLocated
    {
        public int Id { get; }
        public Vector3Int Coords { get; }
        public void HardSetCoords(Vector3Int newCoords);
        public void SwapPosition(ISpaceLocated coordsLocated);
    }
}