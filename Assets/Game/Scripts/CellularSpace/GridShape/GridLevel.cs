using System.Collections.Generic;
using Game.Scripts.View.CellObjects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Scripts.CellularSpace.GridShape
{
    [RequireComponent(typeof(Tilemap))]
    public class GridLevel : MonoBehaviour
    {
        private Tilemap _tilemap;
        private AbstractMonoCellObject[] _gameCellObjects;
            
        private void Awake()
        {
            _tilemap = GetComponent<Tilemap>();
            _gameCellObjects = _tilemap.GetComponentsInChildren<AbstractMonoCellObject>();
        }

        public IEnumerable<AbstractMonoCellObject> GetGameCellObjects() => _gameCellObjects;
    }
}