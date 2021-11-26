using System.Collections;
using Assets.Siege.Model.CellularSpace.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Shapers
{
    public class BlockGridHierarchy: IGridHierarchy
    {
        private readonly Grid _grid;

        [Inject]
        public BlockGridHierarchy(Grid grid)
        {
            _grid = grid;
        }

        public Transform GetChild(int index) => _grid.transform.GetChild(index);
        public IEnumerable GetChildren() => _grid.transform;
        public Tilemap GetLevel(int index)
        {
            throw new System.NotImplementedException();
        }
    }
}