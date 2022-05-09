using System;
using UnityEngine;

namespace Game.Scripts.PathFind
{
    public static class Heuristics
    {
        public static float EuclidHeuristic(Vector3Int c1, Vector3Int c2) =>
            (float) Math.Sqrt(Math.Pow(c1.x - c2.x, 2) + Math.Pow(c1.y - c2.y, 2) + Math.Pow(c1.z - c2.z, 2));
        public static float ManhattanHeuristic(Vector3Int c1, Vector3Int c2) =>
            Math.Abs(c1.x - c2.x) + Math.Abs(c1.y - c2.y) + Math.Abs(c1.z - c2.z);
    }
}