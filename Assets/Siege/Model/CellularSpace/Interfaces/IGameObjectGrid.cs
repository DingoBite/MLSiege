using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IGameObjectGrid
    {
        public Grid GetGrid();
        public void CreateLevels(int height);
        public Tilemap GetLevel(int index);
        public IEnumerable<Tilemap> GetLevels();
    }
}