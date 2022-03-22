using System.Collections.Generic;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.View.CellObjects;
using Game.Scripts.View.GridLevels;
using UnityEngine;

namespace Game.Scripts.CellularSpace.GridShape
{
    public class GridLevelsManager : IGridLevelsManager
    {
        private readonly GridLevel _prefab;
        private readonly List<GridLevel> _levels;
        private Grid _grid;

        public GridLevelsManager(GridLevel prefab)
        {
            _prefab = prefab;
            _levels = new List<GridLevel>();
        }

        public void Init(Grid grid)
        {
            _grid = grid;
            var cellSize = _grid.cellSize;
            CellSize = new Vector3(cellSize.x, cellSize.z, cellSize.y);
            LevelsInit();
        }

        public int LevelsCount => _levels.Count;
        public Vector3 CellSize { get; private set; }

        private void LevelsInit()
        {
            _levels.Clear();
            foreach (Transform levelTransform in _grid.transform)
            {
                if (!levelTransform.TryGetComponent(out GridLevel level)) continue;
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
            var transform = level.transform;
            var transformPosition = transform.position;
            transformPosition.y = i * CellSize.y;
            transform.position = transformPosition;
            level.name = $"Level {i}";
            _levels.Add(level);
        }

        private void CreateLevels(int height)
        {
            if (height < _levels.Count) return;

            for (var i = _levels.Count; i <= height; i++)
            {
                CreateLevel(i);
            }
        }

        public AbstractMonoCellObject PutIntoLevel(int level, AbstractMonoCellObject prefab, Vector3 position, int id)
        {
            var gameObjectMono = Object.Instantiate(prefab, position, Quaternion.identity, GetLevel(level).transform);
            gameObjectMono.name = gameObjectMono.ToString();
            return gameObjectMono;
        }

        private GridLevel GetLevel(int index)
        {
            if (index < _levels.Count) return _levels[index];

            CreateLevels(index);
            return _levels[index];
        }

        public IEnumerable<GridLevel> GetLevels() => _levels;
    }
}