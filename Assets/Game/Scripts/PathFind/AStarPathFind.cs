using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.General;
using Game.Scripts.General.Extensions;
using Game.Scripts.General.Interfaces;
using Grpc.Core.Logging;
using UnityEngine;

namespace Game.Scripts.PathFind
{
    public static class AStarPathFind
    {
        public static Dictionary<TCoordsLocated, TCostValue> FindPath<TCoordsLocated, TCostValue>(
            Vector3Int startCoords, Vector3Int endCoords,
            IEnumerable<Vector3Int> neighbors,
            Func<TCostValue, TCostValue, TCostValue> costValuePlus,
            Delegates.TryGet<Vector3Int, TCoordsLocated> tryGetCellFunc,
            Func<TCoordsLocated, TCoordsLocated, TCostValue> costFunc,
            Func<Vector3Int, Vector3Int, float> heuristic,
            Func<(TCostValue, float), float> heuristicRangeToGoal,
            TCostValue defaultCost, int findStepsCount = -1)
            where TCoordsLocated : ICoordsLocated
            where TCostValue : IComparable, ILimitedValue<TCostValue>
        {
            if (!tryGetCellFunc(startCoords, out var startCell))
                throw new ArgumentException($"{nameof(startCoords)} ");
            if(!tryGetCellFunc(endCoords, out var endCell))
                throw new ArgumentException($"{nameof(endCoords)} ");
            
            var neighborsArray = neighbors as Vector3Int[] ?? neighbors.ToArray();
            var visitedCells = new Dictionary<TCoordsLocated, (TCostValue, float)>();
            var unVisitedCells = new HashSet<TCoordsLocated>();
            var resultPath = new Dictionary<TCoordsLocated, TCostValue>(); 
            
            visitedCells.Add(startCell, (defaultCost, heuristic(startCoords, endCoords)));
            unVisitedCells.Add(startCell);
            var stepsCount = 0;
            while (unVisitedCells.Count > 0 || stepsCount >= findStepsCount)
            {
                stepsCount++;
                var currentCell = unVisitedCells.MinBy(x => heuristicRangeToGoal(visitedCells[x]));
                var currentCost = visitedCells[currentCell].Item1;
                if (resultPath.ContainsKey(currentCell))
                    resultPath[currentCell] = currentCost;
                else
                    resultPath.Add(currentCell, currentCost);
                
                if (currentCell.Equals(endCell))
                    break;
                unVisitedCells.Remove(currentCell);
                foreach (var neighbor in neighborsArray)
                {
                    var newCoords = currentCell.Coords + neighbor;
                    if(!tryGetCellFunc(newCoords, out var neighborCell)) continue;
                    
                    var costChange = costFunc(currentCell, neighborCell);
                    var cost = costValuePlus(currentCost, costChange);
                    
                    if (visitedCells.ContainsKey(neighborCell))
                    {
                        if (cost.CompareTo(visitedCells[neighborCell].Item1) >= 0) continue;
                        visitedCells[neighborCell] = (cost, visitedCells[neighborCell].Item2);
                    }
                    else visitedCells.Add(neighborCell, (cost, heuristic(newCoords, endCoords)));
                    
                    if (!unVisitedCells.Contains(neighborCell) && !cost.Equals(cost.MaxValue)) unVisitedCells.Add(neighborCell);
                }
            }
            
            resultPath.Remove(startCell);
            return resultPath;
        }
        
        
        // public static IEnumerable<(TCoordsLocated, TCostValue)> FindPath<TCoordsLocated, TCostValue>(
        //     Vector3Int startCoords, Vector3Int endCoords,
        //     IEnumerable<Vector3Int> neighbors,
        //     Func<TCostValue, TCostValue, TCostValue> costValuePlus,
        //     Delegates.TryGet<Vector3Int, TCoordsLocated> tryGetCellFunc,
        //     Func<TCoordsLocated, TCoordsLocated, TCostValue> costFunc,
        //     Func<Vector3Int, Vector3Int, float> heuristic,
        //     Func<(TCostValue, float), float> heuristicRangeToGoal,
        //     TCostValue defaultCost, int findStepsCount = -1)
        //     where TCoordsLocated : ICoordsLocated
        //     where TCostValue : IComparable, ILimitedValue<TCostValue>
        // {
        //     var neighborsArray = neighbors as Vector3Int[] ?? neighbors.ToArray();
        //     var visitedCells = new Dictionary<TCoordsLocated, (TCostValue, float)>();
        //     var unVisitedCells = new HashSet<TCoordsLocated>();
        //     var resultPath = new Dictionary<TCoordsLocated, TCostValue>(); 
        //
        //     if (!tryGetCellFunc(startCoords, out var startCell))
        //         throw new ArgumentException($"{nameof(startCoords)} ");
        //     if(!tryGetCellFunc(endCoords, out var endCell))
        //         throw new ArgumentException($"{nameof(endCoords)} ");
        //
        //     visitedCells.Add(startCell, (defaultCost, heuristic(startCoords, endCoords)));
        //     unVisitedCells.Add(startCell);
        //     var stepsCount = 0;
        //     while (unVisitedCells.Count > 0 && stepsCount < findStepsCount)
        //     {
        //         var currentCell = unVisitedCells.MinBy(x => heuristicRangeToGoal(visitedCells[x]));
        //         var currentCost = visitedCells[currentCell].Item1;
        //         if (resultPath.ContainsKey(currentCell))
        //             resultPath[currentCell] = currentCost;
        //         else
        //             resultPath.Add(currentCell, currentCost);
        //         
        //         if (currentCell.Equals(endCell)) break;
        //         unVisitedCells.Remove(currentCell);
        //         var isDeadEnd = true;
        //         foreach (var neighbor in neighborsArray)
        //         {
        //             stepsCount++;
        //             var newCoords = currentCell.Coords + neighbor;
        //             if(!tryGetCellFunc(newCoords, out var neighborCell)) continue;
        //             
        //             var costChange = costFunc(currentCell, neighborCell);
        //             var cost = costValuePlus(currentCost, costChange);
        //             
        //             if (isDeadEnd && !cost.Equals(cost.MaxValue)) isDeadEnd = false;
        //             
        //             if (visitedCells.ContainsKey(neighborCell))
        //             {
        //                 if (cost.CompareTo(visitedCells[neighborCell].Item1) >= 0) continue;
        //                 visitedCells[neighborCell] = (cost, visitedCells[neighborCell].Item2);
        //             }
        //             else visitedCells.Add(neighborCell, (cost, heuristic(newCoords, endCoords)));
        //
        //             if (!unVisitedCells.Contains(neighborCell)) unVisitedCells.Add(neighborCell);
        //         }
        //         if (isDeadEnd) break;
        //     }
        //
        //     resultPath.Remove(startCell);
        //     return resultPath.Select(p => (p.Key, p.Value));
        // }
    }
}