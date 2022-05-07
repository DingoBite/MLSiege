using System.Collections.Generic;
using Game.Scripts.CellularSpace.GridShape;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.CellularSpace
{
    [RequireComponent(typeof(Grid))]
    public class GridWithLevels : MonoBehaviour
    {
        [SerializeField] private GridLevel _gridLevelPrefab;

        private Grid _grid;
        private LinkedList<GridLevel> _levels;

        private int _levelId;
        
        private void Start()
        {
            _grid = GetComponent<Grid>();
            var gridLevels = GetComponentsInChildren<GridLevel>();
            _levels = new LinkedList<GridLevel>(gridLevels);
            _levelId = gridLevels.Length;
        }

        public void Init()
        {
            _grid = GetComponent<Grid>();
            var gridLevels = GetComponentsInChildren<GridLevel>();
            _levels = new LinkedList<GridLevel>(gridLevels);
            _levelId = gridLevels.Length;
        }
        
        public void AddLevel()
        {
            if (_levels == null)  _levels = new LinkedList<GridLevel>();
            var level = Instantiate(_gridLevelPrefab, transform);
            level.transform.position = new Vector3(0, _levelId * _grid.cellSize.y, 0);
            level.name = $"Level {_levelId}";
            _levels.AddLast(level);
            _levelId++;
        }

        public void RemoveLevel()
        {
            if (_levels == null) return;
            DestroyImmediate(_levels.Last.Value.gameObject);
            _levels.RemoveLast();
            _levelId--;
        }

        public void ClearLevels()
        {
            if (_levels == null) return;
            foreach (var level in _levels)
            {
                DestroyImmediate(level.gameObject);
            }
            _levels = null;
            _levelId = 0;
        }
    }
#if UNITY_EDITOR
    [CustomEditor(typeof(GridWithLevels))]
    public class LevelsOnGridManager : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var gridWithLevels = (GridWithLevels) target;
            if (GUILayout.Button("Initialize"))
            {
                gridWithLevels.Init();                
            }
            else if (GUILayout.Button("Add level"))
            {
                gridWithLevels.AddLevel();                
            }
            else if (GUILayout.Button("Remove level"))
            {
                gridWithLevels.RemoveLevel();
            }
            else if (GUILayout.Button("Clear Levels"))
            {
                gridWithLevels.ClearLevels();
            }
        }
    }
#endif
}