using System;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.GridShape.CoordsConverters
{
    public class GridCoordsConverter: IGridCoordsConverter
    {
        private const float Epsilon = Vector3.kEpsilon;
        private Vector3 _cellSize;
        private Vector3 _offset;

        public void Init(Vector3 cellSize)
        {
            _cellSize = cellSize;
            _offset = new Vector3(
                _cellSize.x / 2,
                _cellSize.y / 2,
                _cellSize.z / 2
            );
        }

        public Vector3Int Convert(Vector3 coords) =>
            new Vector3Int(
                (int) Math.Floor(coords.x / _cellSize.x + _offset.x - Epsilon),
                (int) Math.Floor(coords.y / _cellSize.y + _offset.y - Epsilon),
                (int) Math.Floor(coords.z / _cellSize.z + _offset.z - Epsilon)
            );

        public Vector3 Convert(Vector3Int coords) =>
            new Vector3(
                coords.x * _cellSize.x + _offset.x,
                coords.y * _cellSize.y + _offset.y,
                coords.z * _cellSize.z + _offset.z
            );
    }
}