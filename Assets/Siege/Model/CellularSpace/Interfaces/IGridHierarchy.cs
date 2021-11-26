using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Siege.Model.CellularSpace.Interfaces
{
    public interface IGridHierarchy
    {
        public Transform GetChild(int index);
        public IEnumerable GetChildren();
        public Tilemap GetLevel(int index);
    }
}