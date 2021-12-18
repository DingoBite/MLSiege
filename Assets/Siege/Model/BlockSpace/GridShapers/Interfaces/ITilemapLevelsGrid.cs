using System.Collections.Generic;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Siege.Model.BlockSpace.GridShapers.Interfaces
{
    public interface ITilemapLevelsGrid<TMono> where TMono : ActableMono
    {
        public void Init(Grid grid);
        public Vector3 GetCellSize();
        public TMono PutIntoLevel(int level, TMono prefab, Vector3 position, int id);
        public IEnumerable<Tilemap> GetLevels();
    }
}