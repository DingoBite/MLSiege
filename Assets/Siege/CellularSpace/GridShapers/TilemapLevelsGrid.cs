using System.Collections.Generic;
using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Siege.CellularSpace.GridShapers
{
    public class TilemapLevelsGrid<TMono> : ITilemapLevelsGrid<TMono> where TMono : ActableMono
    {
        private readonly Tilemap _prefab;
        private readonly List<Tilemap> _levels;
        private Grid _grid;

        [Inject]
        public TilemapLevelsGrid(Tilemap prefab)
        {
            _prefab = prefab;
            _levels = new List<Tilemap>();
        }

        public void Init(Grid grid)
        {
            _grid = grid;
            LevelsInit();
        }

        private void LevelsInit()
        {
            _levels.Clear();
            foreach (Transform tilemap in _grid.transform)
            {
                var containsTilemap = tilemap.TryGetComponent(out Tilemap level);
                if (!containsTilemap) continue;
                _levels.Add(level);
            }
            _levels.Sort((l1, l2) => l1.transform.position.y.CompareTo(l2.transform.position.y));
            var i = 0;
            foreach (var level in _levels)
            {
                level.name = $"Level {i++}";
            }
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