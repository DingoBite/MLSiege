using System.Collections.Generic;
using Game.Scripts.CellularSpace.General.Enums;
using UnityEngine;

namespace Game.Scripts.CellularSpace.General
{
    public static class VectorIntDirection
    {
        private static readonly Dictionary<Direction, Vector3Int> Directions;

        static VectorIntDirection()
        {
            Directions = new Dictionary<Direction, Vector3Int>
            {
                {Direction.North, Vector3Int.forward},
                {Direction.West, Vector3Int.left},
                {Direction.South, Vector3Int.back},
                {Direction.East, Vector3Int.right},
                {Direction.Stop, Vector3Int.zero}
            };
        }

        public static Vector3Int GetVector(Direction direction) => Directions[direction];
    }
}