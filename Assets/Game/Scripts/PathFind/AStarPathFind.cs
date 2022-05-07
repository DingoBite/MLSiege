using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.General.Extensions;
using Game.Scripts.General.Interfaces;
using UnityEngine;

namespace Game.Scripts.PathFind
{
    public partial class AStarPathFind
    {
        private readonly Func<Vector3Int, Vector3Int, float> _heuristic;

        public AStarPathFind(Func<Vector3Int, Vector3Int, float> heuristic)
        {
            _heuristic = heuristic;
        }
        
        public IEnumerable<(TCoordsLocated, TCostValue)> FindPath<TCoordsLocated, TCostValue>(
            Vector3Int startCoords, Vector3Int endCoords,
            IEnumerable<Vector3Int> neighbors,
            Func<TCostValue, TCostValue, TCostValue> costValuePlus, Delegates.TryGet<Vector3Int, TCoordsLocated> tryGetCellFunc,
            Func<TCoordsLocated, TCoordsLocated, TCostValue> costFunc,
            TCostValue defaultCost)
            where TCoordsLocated : ICoordsLocated
            where TCostValue : IComparable
        {
            return FindPath(startCoords, endCoords, neighbors, costValuePlus, tryGetCellFunc, costFunc,
                _heuristic, defaultCost);
        }
        
        public IEnumerable<(TCoordsLocated, int)> FindPath<TCoordsLocated>(
            Vector3Int startCoords, Vector3Int endCoords,
            IEnumerable<Vector3Int> neighbors, Delegates.TryGet<Vector3Int, TCoordsLocated> tryGetCellFunc,
            Func<TCoordsLocated, TCoordsLocated, int> costFunc)
            where TCoordsLocated : ICoordsLocated
        {
            return FindPath(
                startCoords, endCoords, neighbors, (a, b) => a + b, tryGetCellFunc, costFunc,
                _heuristic, 0);
        }
        
        public IEnumerable<(TCoordsLocated, float)> FindPath<TCoordsLocated>(
            Vector3Int startCoords, Vector3Int endCoords,
            IEnumerable<Vector3Int> neighbors, Delegates.TryGet<Vector3Int, TCoordsLocated> tryGetCellFunc,
            Func<TCoordsLocated, TCoordsLocated, float> costFunc)
            where TCoordsLocated : ICoordsLocated
        {
            return FindPath(
                startCoords, endCoords, neighbors, (a, b) => a + b, tryGetCellFunc, costFunc,
                _heuristic, 0);
        }
        
        public static IEnumerable<(TCoordsLocated, int)> FindPath<TCoordsLocated>(
            Vector3Int startCoords, Vector3Int endCoords,
            IEnumerable<Vector3Int> neighbors, Delegates.TryGet<Vector3Int, TCoordsLocated> tryGetCellFunc,
            Func<TCoordsLocated, TCoordsLocated, int> costFunc,
            Func<Vector3Int, Vector3Int, float> heuristic)
            where TCoordsLocated : ICoordsLocated
        {
            return FindPath(
                startCoords, endCoords, neighbors, (a, b) => a + b, tryGetCellFunc, costFunc,
                heuristic, 0);
        }
        
        public static IEnumerable<(TCoordsLocated, float)> FindPath<TCoordsLocated>(
            Vector3Int startCoords, Vector3Int endCoords,
            IEnumerable<Vector3Int> neighbors, Delegates.TryGet<Vector3Int, TCoordsLocated> tryGetCellFunc,
            Func<TCoordsLocated, TCoordsLocated, float> costFunc,
            Func<Vector3Int, Vector3Int, float> heuristic)
            where TCoordsLocated : ICoordsLocated
        {
            return FindPath(
                startCoords, endCoords, neighbors, (a, b) => a + b, tryGetCellFunc, costFunc,
                heuristic, 0);
        }

        public static IEnumerable<(TCoordsLocated, TCostValue)> FindPath<TCoordsLocated, TCostValue> (
            Vector3Int startCoords, Vector3Int endCoords,
            IEnumerable<Vector3Int> neighbors,
            Func<TCostValue, TCostValue, TCostValue> costValuePlus, Delegates.TryGet<Vector3Int, TCoordsLocated> tryGetCellFunc,
            Func<TCoordsLocated, TCoordsLocated, TCostValue> costFunc,
            Func<Vector3Int, Vector3Int, float> heuristic,
            TCostValue defaultCost)
            where TCoordsLocated : ICoordsLocated
            where TCostValue : IComparable
        {
            var neighborsArray = neighbors as Vector3Int[] ?? neighbors.ToArray();
            var visitedCells = new Dictionary<TCoordsLocated, (TCostValue, float)>();
            var unVisitedCells = new HashSet<TCoordsLocated>();
            var resultPath = new List<(TCoordsLocated, TCostValue)>(); 

            if (!tryGetCellFunc(startCoords, out var startCell))
                throw new ArgumentException($"{nameof(startCoords)} ");
            if(!tryGetCellFunc(endCoords, out var endCell))
                throw new ArgumentException($"{nameof(endCoords)} ");

            visitedCells.Add(startCell, (defaultCost, heuristic(startCoords, endCoords)));
            unVisitedCells.Add(startCell);
            var isFirstStep = true;
            while (unVisitedCells.Count > 0)
            {
                var currentCell = unVisitedCells.MinBy(x => visitedCells[x].Item2);
                if (isFirstStep)
                    isFirstStep = false;
                else
                    resultPath.Add((currentCell, visitedCells[currentCell].Item1));
                if (currentCell.Equals(endCell)) return resultPath;
                unVisitedCells.Remove(currentCell);
                foreach (var neighbor in neighborsArray)
                {
                    var newCoords = currentCell.Coords + neighbor;
                    if(!tryGetCellFunc(newCoords, out var neighborCell)) continue;
                    var costChange = costFunc(currentCell, neighborCell);
                    if (costChange == null) continue;
                    var cost = costValuePlus(visitedCells[currentCell].Item1, costChange);
                    if (visitedCells.ContainsKey(neighborCell))
                    {
                        if (cost.CompareTo(visitedCells[neighborCell].Item1) >= 0) continue;
                        visitedCells[neighborCell] = (cost, heuristic(newCoords, endCoords));
                    }
                    else visitedCells.Add(neighborCell, (cost, heuristic(newCoords, endCoords)));
                    
                    if (!unVisitedCells.Contains(neighborCell)) unVisitedCells.Add(neighborCell);
                }
            }
            return resultPath;
        }
    }
}