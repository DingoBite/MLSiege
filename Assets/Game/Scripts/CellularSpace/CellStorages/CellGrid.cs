using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace.CellStorages.CellObjects;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridShape.CoordsConverters.Interfaces;
using Game.Scripts.CellularSpace.GridShape.Interfaces;
using Game.Scripts.General.Repos;
using Game.Scripts.View.CellObjects;
using UnityEngine;

namespace Game.Scripts.CellularSpace.CellStorages
{
    public class CellGrid : ICellGrid
    {
        private readonly IdRepositoryWithFactory<AbstractChildCellObject> _cellObjectsRepository = new IdRepositoryWithFactory<AbstractChildCellObject>();
        private List<List<List<ICellMutable>>> _cells;
        private Vector3Int _minFormingPoint;
        private Vector3Int _maxFormingPoint;
        private Vector3Int _sizeVector;
        private IGridCoordsConverter _gridCoordsConverter;
        
        public void Init(IGridLevelsManager gridLevelsManager, IGridCoordsConverter gridCoordsConverter)
        {
            _gridCoordsConverter = gridCoordsConverter;
            _minFormingPoint = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);
            _maxFormingPoint = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            var monoCellObjects = new Dictionary<Vector3Int, AbstractMonoCellObject>();
            foreach (var level in gridLevelsManager.GetLevels())
            {
                foreach (var cellObject in level.GetGameCellObjects())
                {
                    var coords = _gridCoordsConverter.Convert(cellObject.transform.position);
                    monoCellObjects.Add(coords, cellObject);
                    if (_minFormingPoint.x > coords.x) _minFormingPoint.x = coords.x;
                    if (_minFormingPoint.y > coords.y) _minFormingPoint.y = coords.y;
                    if (_minFormingPoint.z > coords.z) _minFormingPoint.z = coords.z;
                    if (_maxFormingPoint.x < coords.x) _maxFormingPoint.x = coords.x;
                    if (_maxFormingPoint.y < coords.y) _maxFormingPoint.y = coords.y;
                    if (_maxFormingPoint.z < coords.z) _maxFormingPoint.z = coords.z;
                }
            }
            _sizeVector = _maxFormingPoint - _minFormingPoint + Vector3Int.one;
            InitAllocate(monoCellObjects);
        }

        public bool TryGetCellObject(int id, out AbstractCellObject cellObject)
        {
            if (_cellObjectsRepository.Contains(id))
            {
                cellObject = _cellObjectsRepository.Get(id);
                return true;
            }
            cellObject = null;
            return false;
        }

        public bool TryGetCell(Vector3Int coords, out ICell cell)
        {
            if (!IsAchievableCell(coords))
                throw new ArgumentOutOfRangeException();
            coords = CoordsToIndexCoords(coords);
            return TryGetCell(coords.x, coords.y, coords.z, out cell);
        }

        public AbstractCellObject GetCellObject(int id) => _cellObjectsRepository.Get(id);

        public ICell GetCell(Vector3Int coords) => _cells[coords.x][coords.y][coords.z];

        public bool TrySetCellObjectTo(Vector3Int coords, int cellObjectId)
        {
            coords = CoordsToIndexCoords(coords);
            if (!_cellObjectsRepository.Contains(cellObjectId)) return false;
            if (!IsAchievableCell(coords)) return false;
            
            var cellObject = _cellObjectsRepository.Get(cellObjectId);
            _cells[coords.x][coords.y][coords.z].SetCellObject(cellObject);
            return true;
        }

        public void SetCell(Vector3Int coords, int cellObjectId) =>
            _cells[coords.x][coords.y][coords.z].SetCellObject(_cellObjectsRepository.Get(cellObjectId));

        public IEnumerable<ICell> GetCells() => 
            from x in _cells from xy in x from xyz in xy select xyz;

        private void InitAllocate(IReadOnlyDictionary<Vector3Int, AbstractMonoCellObject> monoCellObjects)
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
                        AllocateCellObject(i, j, k, monoCellObjects);
                    }
                }
            }
        }

        private void AllocateCellObject(int i, int j, int k, IReadOnlyDictionary<Vector3Int, AbstractMonoCellObject> monoCellObjects)
        {
            var coords = new Vector3Int(i, j, k) + _minFormingPoint;
            Cell cell;
            if (monoCellObjects.TryGetValue(coords, out var monoCellObject))
            {
                var isExternallyModifiable = monoCellObject.IsExternallyModifiable;
                cell = new Cell(this, coords, id => _cellObjectsRepository.Remove(id));
                var cellBlock = _cellObjectsRepository.MakeAndAdd(id =>
                    new CellBlock(id, (sender, actParams) =>
                        monoCellObject.CommitAction(sender, actParams), isExternallyModifiable));
                
                cell.SetCellObject(cellBlock);
                monoCellObject.Init(cellBlock.Id, _gridCoordsConverter.Convert);
            }
            else
            {
                cell = new Cell(this, coords);
            }
            _cells[i][j].Add(cell);
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
                        _cells[i][j].Add(new Cell(this, coords));
                    }
                }
            }
        }

        private Vector3Int CoordsToIndexCoords(Vector3Int coords) => coords -= _minFormingPoint;
        
        private bool TryGetCell(int x, int y, int z, out ICell cell)
        {
            cell = null;
            if (y < 0) return false;
            if (y >= _cells[x].Count) UpLevelsTo(y);
            cell = _cells[x][y][z];
            return true;
        }

        private bool IsAchievableCell(Vector3Int coords)
        {
            return !(coords.x < _minFormingPoint.x &&
                     coords.x > _maxFormingPoint.x &&
                     coords.y < _minFormingPoint.y &&
                     coords.z < _minFormingPoint.z &&
                     coords.z > _maxFormingPoint.z);
        }
    }
}