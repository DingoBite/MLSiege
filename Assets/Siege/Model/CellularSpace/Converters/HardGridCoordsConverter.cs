using System;
using Assets.Siege.Model.CellularSpace.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Converters
{
    public class HardGridCoordsConverter: IGridCoordsConverter
    {
        private readonly Vector3 _cellSize;
        private readonly Vector3 _offset;
        private readonly float _epsilon;

        // In third dimension Grid y axis in size is z axis.
        // Offset on y dimension (z) is 0.

        [Inject]
        public HardGridCoordsConverter(IGameObjectGrid gameObjectGrid)
        {
            _cellSize = gameObjectGrid.GetGrid().cellSize;
            _offset = new Vector3(
                _cellSize.x / 2,
                _cellSize.z / 2,
                _cellSize.y / 2
            );
            _epsilon = Vector3.kEpsilon;
        }

        public Vector3Int Convert(Vector3 coords)
        {
            return new Vector3Int(
                (int) Math.Floor(coords.x / _cellSize.x + _offset.x - _epsilon),
                (int) Math.Floor(coords.y / _cellSize.z + _offset.y - _epsilon),
                (int) Math.Floor(coords.z / _cellSize.y + _offset.z - _epsilon)
            );
        }

        public Vector3 Convert(Vector3Int coords)
        {
            return new Vector3(
                coords.x * _cellSize.x + _offset.x,
                coords.y * _cellSize.z + _offset.y,
                coords.z * _cellSize.y + _offset.z
            );
        }
    }
}