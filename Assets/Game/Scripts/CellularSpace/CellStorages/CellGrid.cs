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
        private readonly IIdRepository<AbstractCellObject> _cellObjectsRepository = new IdRepository<AbstractCellObject>();
        private List<List<List<ICell>>> _cells;
        private Vector3Int _minFormingPoint;
        private Vector3Int _maxFormingPoint;
        private Vector3Int _sizeVector;
        
        public void Init(IGridLevelsManager gridLevelsManager, IGridCoordsConverter gridCoordsConverter)
        {
            _minFormingPoint = new Vector3Int(int.MaxValue, int.MaxValue, int.MaxValue);
            _maxFormingPoint = new Vector3Int(int.MinValue, int.MinValue, int.MinValue);
            var monoCellObjects = new Dictionary<Vector3Int, AbstractMonoCellObject>();
            foreach (var level in gridLevelsManager.GetLevels())
            {
                foreach (var cellObject in level.GetGameCellObjects())
                {
                    var coords = gridCoordsConverter.Convert(cellObject.transform.position);
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
            _cells = new List<List<List<ICell>>>();
            for (var i = 0; i < _sizeVector.x; i++)
            {
                _cells.Add(new List<List<ICell>>());
                for (var j = 0; j < _sizeVector.y; j++)
                {
                    _cells[i].Add(new List<ICell>());
                    for (var k = 0; k < _sizeVector.z; k++)
                    {
                        var coords = new Vector3Int(i, j, k) + _minFormingPoint;
                        
                        Cell cell;
                        if (monoCellObjects.TryGetValue(coords, out var monoCellObject))
                        {
                            cell = new Cell(this, coords);
                            var id = _cellObjectsRepository.PeekId();
                            var cellBlock = new CellBlock(id, monoCellObject.CommitAction, () =>
                                {
                                    _cellObjectsRepository.Remove(id);
                                    cell.Clear();
                                });
                            _cellObjectsRepository.Add(cellBlock);
                            cell.CellObject = cellBlock;
                        }
                        else
                        {
                            cell = new Cell(this, coords);
                        }
                        _cells[i][j].Add(cell);
                    }
                }
            }
        }

        private void UpLevelsTo(int y)
        {
            if (y < _sizeVector.y) return;
            
            for (var i = 0; i < _sizeVector.x; i++)
            {
                for (var j = _sizeVector.y; j <= y; j++)
                {
                    _cells[i].Add(new List<ICell>());
                    for (var k = 0; k < _sizeVector.z; k++)
                    {
                        var coords = new Vector3Int(i, j, k) + _minFormingPoint;
                        _cells[i][j].Add(new Cell(this, coords));
                    }
                }
            }
        }
        
        private bool TryGetCell(int x, int y, int z, out ICell cell)
        {
            cell = null;
            if (x >= _cells.Count) return false;
            if (y >= _cells[x].Count) UpLevelsTo(y);
            if (z >= _cells[x][y].Count) return false;
            cell = _cells[x][y][z];
            return true;
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
            if (coords.y < _minFormingPoint.y)
                throw new Exception("Try to extend space bellow min forming point");
            coords -=_minFormingPoint;
            return TryGetCell(coords.x, coords.y, coords.z, out cell);
        }

        public IEnumerable<ICell> GetCells() => 
            from x in _cells from xy in x from xyz in xy select xyz;
    }
}