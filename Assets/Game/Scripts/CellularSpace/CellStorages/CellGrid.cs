using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.General.Repos;
using Game.Scripts.View.CellObjects.Serialization;
using Game.Scripts.View.CellObjects.Serialization.Interfaces;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages
{
    public class CellGrid : ICellGrid
    {
        private readonly IdRepoWithFactory<AbstractChildCellObject> _cellObjectsRepo =
            new IdRepoWithFactory<AbstractChildCellObject>();

        private List<List<List<ICellMutable>>> _cells;
        private Vector3Int _minFormingPoint;
        private Vector3Int _maxFormingPoint;
        private Vector3Int _sizeVector;
        private IGridCoordsConverter _gridCoordsConverter;

        public void Init(IGridLevelsManager gridLevelsManager, IGridCoordsConverter gridCoordsConverter, Grid gameGrid)
        {
            _gridCoordsConverter = gridCoordsConverter;
            _minFormingPoint = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);
            _maxFormingPoint = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            var soloCellObjects = new Dictionary<Vector3Int, MonoSoloCellObject>();
            var complexCellObjects = new List<MonoAgent>();
            foreach (var level in gridLevelsManager.GetLevels())
            {
                foreach (var cellObject in level.GetMonoSoloCellObject())
                {
                    var coords = _gridCoordsConverter.Convert(cellObject.MainPosition);
                    soloCellObjects.Add(coords, cellObject);
                    if (_minFormingPoint.x > coords.x) _minFormingPoint.x = coords.x;
                    if (_minFormingPoint.y > coords.y) _minFormingPoint.y = coords.y;
                    if (_minFormingPoint.z > coords.z) _minFormingPoint.z = coords.z;
                    if (_maxFormingPoint.x < coords.x) _maxFormingPoint.x = coords.x;
                    if (_maxFormingPoint.y < coords.y) _maxFormingPoint.y = coords.y;
                    if (_maxFormingPoint.z < coords.z) _maxFormingPoint.z = coords.z;
                }

                foreach (var cellObject in level.GetMonoAgents())
                {
                    var headCoords = _gridCoordsConverter.Convert(cellObject.MainPosition);
                    complexCellObjects.Add(cellObject);
                    if (_maxFormingPoint.y < headCoords.y) _maxFormingPoint.y = headCoords.y;
                }
            }

            _sizeVector = _maxFormingPoint - _minFormingPoint + Vector3Int.one;
            AllocateCellArrays();
            AllocateSoloCellObjects(soloCellObjects, gameGrid);
            AllocateComplexCellObjects(complexCellObjects, gameGrid);
        }

        public bool TryGetCellObject(int id, out AbstractCellObject cellObject)
        {
            if (!TryGetChildCellObject(id, out var childCellObject))
            {
                cellObject = null;
                return false;
            }
            cellObject = childCellObject;
            return true;
        }
        
        private bool TryGetChildCellObject(int id, out AbstractChildCellObject cellObject)
        {
            if (_cellObjectsRepo.Contains(id))
            {
                cellObject = _cellObjectsRepo.Get(id);
                return true;
            }
            cellObject = null;
            return false;
        }
        
        public bool TryGetCell(Vector3Int coords, out ICell cell)
        {
            if (!TryGetCellMutable(coords, out var cellMutable))
            {
                cell = null;
                return false;
            }
            cell = cellMutable;
            return true;
        }
        
        private bool TryGetCellMutable(Vector3Int coords, out ICellMutable cell)
        {
            if (!IsAchievableCell(coords))
            {
                Debug.LogError($"Coords = {coords} are not achievable");
                cell = null;
                return false;
            }

            coords = CoordsToIndex(coords);
            return TryGetCellByIndex(coords.x, coords.y, coords.z, out cell);
        }
        
        private bool TryGetCellByIndex(int x, int y, int z, out ICellMutable cell)
        {
            cell = null;
            if (y < 0) return false;
            if (y >= _cells[x].Count) UpLevelsTo(y);
            cell = _cells[x][y][z];
            return true;
        }

        public AbstractCellObject GetCellObject(int id) => _cellObjectsRepo.Get(id);
        private AbstractChildCellObject GetChildCellObject(int id) => _cellObjectsRepo.Get(id);

        public ICell GetCell(Vector3Int coords) => _cells[coords.x][coords.y][coords.z];
        private ICellMutable GetMutableCell(Vector3Int coords) => _cells[coords.x][coords.y][coords.z];

        public bool TrySetCellObjectTo(Vector3Int coords, int cellObjectId)
        {
            coords = CoordsToIndex(coords);
            if (!_cellObjectsRepo.Contains(cellObjectId)) return false;
            if (!IsAchievableCell(coords)) return false;

            var cellObject = _cellObjectsRepo.Get(cellObjectId);
            _cells[coords.x][coords.y][coords.z].SetCellObject(cellObject);
            return true;
        }

        public bool TrySwapCellObject(Vector3Int coords1, Vector3Int coords2)
        {
            if (!TryGetCellMutable(coords1, out var cell1))
                return false;
            if (!TryGetCellMutable(coords2, out var cell2))
                return false;
            var cellObject = cell1.ChildCellObject;
            cell1.SetCellObject(cell2.ChildCellObject);
            cell2.SetCellObject(cellObject);
            return true;
        }

        public bool TryMoveCellObjectTo(Vector3Int coords, int cellObjectId)
        {
            if (!TryGetCellMutable(coords, out var cell))
                return false;
            if (!cell.IsEmpty) return false;
            if (!TryGetChildCellObject(cellObjectId, out var cellObject))
                return false;
            var cellObjectCell = cellObject.ParentCell;
            cell.SetCellObject(cellObject);
            cellObjectCell.SetCellObject(null);
            return true;
        }

        public void SetCellObjectToCoords(Vector3Int coords, int cellObjectId)
        {
            coords = CoordsToIndex(coords);
            _cells[coords.x][coords.y][coords.z].SetCellObject(_cellObjectsRepo.Get(cellObjectId));
        }

        public IEnumerable<ICell> GetCells() =>
            from x in _cells from xy in x from xyz in xy select xyz;
        
        private Vector3Int CoordsToIndex(Vector3Int coords) => coords - _minFormingPoint;
        private Vector3Int IndexToCoords(Vector3Int coords) => coords + _minFormingPoint;

        private bool IsAchievableCell(Vector3Int coords)
        {
            return !(coords.x < _minFormingPoint.x &&
                     coords.x > _maxFormingPoint.x &&
                     coords.y < _minFormingPoint.y &&
                     coords.z < _minFormingPoint.z &&
                     coords.z > _maxFormingPoint.z);
        }

        private void AllocateCellArrays()
        {
            _cells = new List<List<List<ICellMutable>>>();
            for (var i = 0; i < _sizeVector.x; i++)
            {
                _cells.Add(new List<List<ICellMutable>>());
                for (var j = 0; j < _sizeVector.y; j++)
                {
                    _cells[i].Add(new List<ICellMutable>());
                    for (var k = 0; k < _sizeVector.z; k++)
                    {
                        AllocateCell(i, j, k);
                    }
                }
            }
        }

        private void AllocateCell(int i, int j, int k)
        {
            var coords = IndexToCoords(new Vector3Int(i, j, k));
            var cell = new Cell(this, coords, id => _cellObjectsRepo.Remove(id));
            _cells[i][j].Add(cell);
        }

        private void AllocateSoloCellObjects(IReadOnlyDictionary<Vector3Int, MonoSoloCellObject> monoCellObjects, Grid gameGrid)
        {
            foreach (var monoCellObject in monoCellObjects)
            {
                var coords = CoordsToIndex(monoCellObject.Key);
                if (!IsAchievableCell(coords))
                    throw new ArgumentOutOfRangeException(
                        $"Coords = {monoCellObject.Key} are not achievable in space");
                AllocateSoloCellObject(coords, monoCellObject.Value, gameGrid);
            }
        }

        private void AllocateSoloCellObject(Vector3Int coords, MonoSoloCellObject monoCellObject, Grid gameGrid)
        {
            var cell = GetMutableCell(coords);
            var cellObject = 
                _cellObjectsRepo.MakeAndAdd(monoCellObject.MakeCellObjectFunc(gameGrid, _gridCoordsConverter.Convert));
            
            cell.SetCellObject(cellObject);
        }

        private void AllocateComplexCellObjects(IEnumerable<MonoAgent> monoCellObjects, Grid gameGrid)
        {
            foreach (var monoCellObject in monoCellObjects)
            {
                AllocateComplexCellObject(monoCellObject, gameGrid);
            }
        }

        private void AllocateComplexCellObject(MonoAgent monoCellObject, Grid gameGrid)
        {
            var headCoords = CoordsToIndex(_gridCoordsConverter.Convert(monoCellObject.MainPosition));
            var headCell = GetMutableCell(headCoords);
            var legCoords = CoordsToIndex(_gridCoordsConverter.Convert(monoCellObject.LegsPosition));
            var legsCell = GetMutableCell(legCoords);
            
            var (cellAgentHead, cellAgentLegs) = 
                _cellObjectsRepo.MakeAndAdd(monoCellObject.MakeCellAgentFunc(gameGrid, _gridCoordsConverter.Convert));
            
            headCell.SetCellObject(cellAgentHead);
            legsCell.SetCellObject(cellAgentLegs);
        }

        private void UpLevelsTo(int y)
        {
            if (y < _sizeVector.y) return;

            for (var i = 0; i < _sizeVector.x; i++)
            {
                for (var j = _sizeVector.y; j <= y; j++)
                {
                    _cells[i].Add(new List<ICellMutable>());
                    for (var k = 0; k < _sizeVector.z; k++)
                    {
                        var coords = new Vector3Int(i, j, k) + _minFormingPoint;
                        _cells[i][j].Add(new Cell(this, coords, id => _cellObjectsRepo.Remove(id)));
                    }
                }
            }
        }
    }
}