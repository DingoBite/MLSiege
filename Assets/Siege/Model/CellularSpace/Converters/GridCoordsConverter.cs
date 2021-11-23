using Assets.Siege.Model.CellularSpace.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Converters
{
    public class GridCoordsConverter: IGridCoordsConverter
    {
        private readonly Vector3 _cellSize;
        private readonly Vector3 _offset;

        // In third dimension Grid y axis in size is z axis.
        // Offset on y dimension (z) is 0.
        public GridCoordsConverter([Inject] Grid grid)
        {
            _cellSize = grid.cellSize;
            _offset = new Vector3(
                _cellSize.x / 2 + 2 * float.Epsilon, 
                _cellSize.z / 2 + 2 * float.Epsilon,
                float.Epsilon
                );
        }

        public Vector3Int Convert(Vector3 coords)
        {
            return new Vector3Int(
                (int) (coords.x / _cellSize.x + _offset.x),
                (int) (coords.y / _cellSize.z + _offset.z),
                (int) (coords.z / _cellSize.y + _offset.y)
            );
        }

        public Vector3 Convert(Vector3Int coords)
        {
            return new Vector3(
                coords.x * _cellSize.x + _offset.x,
                coords.y * _cellSize.z + _offset.z,
                coords.z * _cellSize.y + _offset.y
            );
        }
    }
}