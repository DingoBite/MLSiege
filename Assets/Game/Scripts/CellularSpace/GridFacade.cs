using Game.Scripts.CellularSpace.CellStorages;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.General.StaticUtils.Enums;
using UnityEngine;
using Zenject;

namespace Game.Scripts.CellularSpace.GridShape
{
    public class GridFacade : IGridFacade
    {
        private IGridLevelsManager _gridLevelsManager;
        private IGridCoordsConverter _gridCoordsConverter;
        private ICellGrid _cellGrid;
        private FlexibleData _cashedActionCommitParams = new FlexibleData();
        
        [Inject]
        public GridFacade(
            IGridLevelsManager gridLevelsManager,
            IGridCoordsConverter gridCoordsConverter,
            ICellGrid cellGrid)
        {
            _gridLevelsManager = gridLevelsManager;
            _gridCoordsConverter = gridCoordsConverter;
            _cellGrid = cellGrid;
        }

        public void Init(Grid grid)
        {
            _gridLevelsManager.Init(grid);
            _gridCoordsConverter.Init(_gridLevelsManager.CellSize);
            _cellGrid.Init(_gridLevelsManager, _gridCoordsConverter);
        }

        public FlexibleData CommitAction(Vector3 position)
        {
            if (!_cellGrid.TryGetCell(_gridCoordsConverter.Convert(position), out var cell)) return null;
            
            var rnd = Random.Range(0, 3);
            _cashedActionCommitParams.SetIntParam("ActionType", rnd);
            return cell.CellObject.CommitAction(_cashedActionCommitParams);

        }
    }
}