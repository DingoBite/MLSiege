using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.GridShapers
{
    public class TilemapLevelsGrid<TMono> : ITilemapLevelsGrid<TMono> where TMono : ActableMono
    {
        private readonly Grid _grid;
        private readonly Tilemap _prefab;
        private readonly List<Tilemap> _levels;

        
        public TilemapLevelsGrid([Inject] Tilemap prefab, Grid grid)
        {
            _prefab = prefab;
            _grid = grid;
            _levels = LevelsInit();
        }

        private List<Tilemap> LevelsInit()
        {
            var levels = new List<Tilemap>();
            foreach (Transform tilemap in _grid.transform)
            {
                var containsTilemap = tilemap.TryGetComponent(out Tilemap level);
                if (!containsTilemap) continue;
                levels.Add(level);
            }
            levels.Sort((l1, l2) => l1.transform.position.y.CompareTo(l2.transform.position.y));
            var i = 0;
            foreach (var level in levels)
            {
                level.name = $"Level {i++}";
            }

            return levels;
        }

        private void CreateLevel(int i)
        {
            var level = Object.Instantiate(_prefab, _grid.transform);
            var transformPosition = level.transform.position;
            transformPosition.y += _grid.cellSize.z;
            level.transform.position = transformPosition;
            level.name = $"Level {i}";
            _levels.Add(level.GetComponent<Tilemap>());
        }

        private void CreateLevels(int height)
        {
            if (height < _levels.Count) return;

            for (var i = _levels.Count; i <= height; i++)
            {
                CreateLevel(i);
            }
        }

        public Vector3 GetCellSize() => _grid.cellSize;

        private Tilemap GetLevel(int index)
        {
            if (index < _levels.Count) return _levels[index];

            CreateLevels(index);
            return _levels[index];
        }

        public TMono PutIntoLevel(int level, TMono prefab, Vector3 position, int id)
        {
            var gameObjectMono = Object.Instantiate(prefab, position, Quaternion.identity, GetLevel(level).transform);
            var mono = gameObjectMono.GetComponent<TMono>();
            mono.Id = id;
            gameObjectMono.name = mono.ToString();
            return mono;
        }

        public IEnumerable<Tilemap> GetLevels() => _levels;
    }
}