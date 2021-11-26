using Assets.Siege.Model.CellularSpace.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Converters
{
    public class SoftGridCoordsConverter: IGridCoordsConverter
    {
        private readonly IGameObjectGrid _gameObjectGrid;

        [Inject]
        public SoftGridCoordsConverter(IGameObjectGrid gameObjectGrid)
        {
            _gameObjectGrid = gameObjectGrid;
        }

        public Vector3Int Convert(Vector3 position) => _gameObjectGrid.GetGrid().WorldToCell(position);

        public Vector3 Convert(Vector3Int coords)
        {
            var position = _gameObjectGrid.GetLevel(coords.z).CellToWorld(coords) + _gameObjectGrid.GetGrid().cellSize / 2;
            var y = position.z;
            position.z = position.y;
            position.y = y;
            return position;
        }
    }
}