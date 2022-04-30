using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.General.Extensions;
using Game.Scripts.General.Interfaces;
using UnityEngine;

namespace Game.Scripts.PathFind
{
    public static class AStarPathFind
    {
        public delegate bool TryGet<in TParam1, TResult>(TParam1 param1, out TResult result);
    
        public static void FindPath<TCoordsLocated>(Vector3Int startCoords, Vector3Int endCoords, IEnumerable<Vector3Int> neighbors,
            TryGet<Vector3Int, TCoordsLocated> tryGetCellFunc,
            Func<TCoordsLocated, TCoordsLocated, int> costFunc,
            Func<Vector3Int, Vector3Int, float> heuristic)
            where TCoordsLocated : ICoordsLocated
        {
            var neighborsArray = neighbors as Vector3Int[] ?? neighbors.ToArray();
            var visitedCells = new Dictionary<TCoordsLocated, (int, float)>();
            var unVisitedCells = new HashSet<TCoordsLocated>();

            if (!tryGetCellFunc(startCoords, out var startCell))
                throw new ArgumentException($"{nameof(startCoords)} ");
            if(!tryGetCellFunc(endCoords, out var endCell))
                throw new ArgumentException($"{nameof(endCoords)} ");

            visitedCells.Add(startCell, (0, heuristic(startCoords, endCoords)));
            unVisitedCells.Add(startCell);
            while (unVisitedCells.Count > 0)
            {
                var currentCell = unVisitedCells.MinBy(x => visitedCells[x].Item2);
                if (currentCell.Equals(endCell)) return;
                unVisitedCells.Remove(currentCell);
                foreach (var neighbor in neighborsArray)
                {
                    var newCoords = currentCell.Coords + neighbor;
                    if(!tryGetCellFunc(newCoords, out var neighborCell)) continue;
                    var cost = visitedCells[currentCell].Item1 + costFunc(currentCell, neighborCell);
                    if (visitedCells.ContainsKey(neighborCell) && cost >= visitedCells[neighborCell].Item1) continue;
                    visitedCells.Add(neighborCell, (cost, heuristic(newCoords, endCoords)));
                    if (!unVisitedCells.Contains(neighborCell)) unVisitedCells.Add(neighborCell);
                }
            }
        }
    }
}