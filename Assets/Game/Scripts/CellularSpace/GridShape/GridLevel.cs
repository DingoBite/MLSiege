using System.Collections.Generic;
using Game.Scripts.View.CellObjects.ViewEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.CellularSpace.GridShape
{
    [RequireComponent(typeof(Tilemap))]
    public class GridLevel : MonoBehaviour
    {
        private Tilemap _tilemap;
        private MonoCellObject[] _monoBlocks;
        private MonoAgent[] _monoAgents;
            
        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
            _monoBlocks = _tilemap.GetComponentsInChildren<MonoCellObject>();
            _monoAgents = _tilemap.GetComponentsInChildren<MonoAgent>();
        }

        public IEnumerable<MonoCellObject> GetMonoSoloCellObject() => _monoBlocks;
        public IEnumerable<MonoAgent> GetMonoAgents() => _monoAgents;
    }
}