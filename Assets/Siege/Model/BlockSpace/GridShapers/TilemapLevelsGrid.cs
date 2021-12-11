using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using Object = UnityEngine.Object;

namespace Assets.Siege.Model.BlockSpace.GridShapers
{
    public class TilemapLevelsGrid: ITilemapLevelsGrid
    {
        private Grid _grid;
        private readonly Tilemap _prefab;
        private List<Tilemap> _levels;

        [Inject]
        public TilemapLevelsGrid(Tilemap prefab)
        {
            _prefab = prefab;
        }

        public void Init(Grid grid)
        {
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

        public Tilemap GetLevel(int index)
        {
            if (index < _levels.Count) return _levels[index];

            CreateLevels(index);
            return _levels[index];
        }

        public IEnumerable<Tilemap> GetLevels() => _levels;
    }
}