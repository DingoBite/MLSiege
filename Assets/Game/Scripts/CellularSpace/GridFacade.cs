using System;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.Time.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.CellularSpace
{
    public class GridFacade : IGridFacade, IUpdatable
    {
        private readonly IGridLevelsManager _gridLevelsManager;
        private readonly IGridCoordsConverter _gridCoordsConverter;
        private readonly ICellGrid _cellGrid;
        private int _selectedCellObjectId = -1;

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

        public void Init(Grid grid, Grid gameGrid)
        {
            _gridLevelsManager.Init(grid);
            _gridCoordsConverter.Init(_gridLevelsManager.CellSize);
            _cellGrid.Init(_gridLevelsManager, _gridCoordsConverter, gameGrid);
        }

        public void CommitSelectAction(int id)
        {
            if (!_cellGrid.TryGetCellObject(id, out var cellObject)) 
                return;
            if (!cellObject.IsModifiable) 
                return;
            if (_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCellObject))
                selectedCellObject.CommitAction(this, CellObjectBaseActions.Unselect);
            
            cellObject.CommitAction(this, CellObjectBaseActions.Select);
            _selectedCellObjectId = cellObject.Id;
        }
        
        public void CommitAction(int id, PerformanceParam performanceData)
        {
            if (performanceData == null) 
                throw new ArgumentNullException(nameof(performanceData));
            if (!_cellGrid.TryGetCellObject(id, out var cellObject)) 
                return;
            if (!cellObject.IsModifiable) 
                return;
            
            cellObject.CommitAction(this, performanceData);
        }

        public void CommitActionToSelected(PerformanceParam performanceData)
        {
            if (!_cellGrid.TryGetCellObject(_selectedCellObjectId, out var selectedCellObject))
                return;
            if (!selectedCellObject.IsModifiable) 
                return;
            selectedCellObject.CommitAction(this, performanceData);
        }
        
        private readonly ActPerformanceParam<CellObjectBaseAction> _applyGravityAction 
            = new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.ApplyGravity);
        
        public void ApplyGlobalAction()
        {
            foreach (var cell in _cellGrid.GetCells().Where(c => !c.IsEmpty && c.CellObject.IsModifiable))
            {
                cell.CellObject.CommitAction(this, _applyGravityAction);
            }
        }

        public void OnUpdate()
        {
            ApplyGlobalAction();
        }
    }
}