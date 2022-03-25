using System;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using UnityEngine;
using Zenject;

namespace Game.Scripts.CellularSpace
{
    public class GridFacade : IGridFacade
    {
        private IGridLevelsManager _gridLevelsManager;
        private IGridCoordsConverter _gridCoordsConverter;
        private ICellGrid _cellGrid;
        private AbstractCellObject _selectedCellObject;
        
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

        public void CommitAction(Vector3 position)
        {
            OnCommitAction();
            if (!_cellGrid.TryGetCell(_gridCoordsConverter.Convert(position), out var cell)) return;
            _selectedCellObject?.CommitAction(new ActionPerformanceData<CellBlockLogicAction>(CellBlockLogicAction.Unselect));
            cell.CellObject.CommitAction(new ActionPerformanceData<CellBlockLogicAction>(CellBlockLogicAction.Select));
            _selectedCellObject = cell.CellObject;
        }
        
        public void CommitAction(Vector3 position, FlexibleData actionPerformanceData)
        {
            OnCommitAction();
            if (actionPerformanceData == null) throw new ArgumentNullException(nameof(actionPerformanceData));
            if (!_cellGrid.TryGetCell(_gridCoordsConverter.Convert(position), out var cell)) return;
            
            cell.CellObject.CommitAction(actionPerformanceData);
        }

        public void CommitAction(FlexibleData actionPerformanceData)
        {
            OnCommitAction();
            _selectedCellObject?.CommitAction(actionPerformanceData);
        }

        private void OnCommitAction()
        {
            _cellGrid.ClearDisposed();
        }
    }
}