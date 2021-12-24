using UnityEngine;

namespace Assets.Siege.CellularSpace.General.Interfaces
{
    public interface ISpaceLocated
    {
        public int Id { get; }
        public Vector3Int Coords { get; }
        public void UnsafeCoordsChange(Vector3Int newCoords);
        public void SwapPosition(ISpaceLocated coordsLocated);
    }
}