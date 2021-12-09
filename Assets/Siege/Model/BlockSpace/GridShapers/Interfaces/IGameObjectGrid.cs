using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Siege.Model.BlockSpace.GridShapers.Interfaces
{
    public interface IGameObjectGrid
    {
        public Vector3 GetCellSize();
        public Tilemap GetLevel(int index);
        public IEnumerable<Tilemap> GetLevels();
    }
}