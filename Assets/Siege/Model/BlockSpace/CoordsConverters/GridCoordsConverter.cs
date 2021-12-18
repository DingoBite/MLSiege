using System;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.CoordsConverters
{
    public class GridCoordsConverter: IGridCoordsConverter
    {
        private Vector3 _cellSize;
        private Vector3 _offset;
        private readonly float _epsilon;

        // In third dimension Grid y axis in size is z axis.
        // Offset on y dimension (z) is 0.

        public GridCoordsConverter()
        {
            _epsilon = Vector3.kEpsilon;

        }

        public void Init(Vector3 cellSize)
        {
            _cellSize = cellSize;
            _offset = new Vector3(
                _cellSize.x / 2,
                _cellSize.z / 2,
                _cellSize.y / 2
            );
        }

        public Vector3Int Convert(Vector3 coords) =>
            new Vector3Int(
                (int) Math.Floor(coords.x / _cellSize.x + _offset.x - _epsilon),
                (int) Math.Floor(coords.y / _cellSize.z + _offset.y - _epsilon),
                (int) Math.Floor(coords.z / _cellSize.y + _offset.z - _epsilon)
            );

        public Vector3 Convert(Vector3Int coords) =>
            new Vector3(
                coords.x * _cellSize.x + _offset.x,
                coords.y * _cellSize.z + _offset.y,
                coords.z * _cellSize.y + _offset.z
            );
    }
}