using System.Collections.Generic;
using Assets.Siege.Model.General.Enums;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.CoordsConverters
{
    public static class VectorIntFromDirection
    {
        private static readonly Dictionary<Direction, Vector3Int> Directions;

        static VectorIntFromDirection()
        {
            Directions = new Dictionary<Direction, Vector3Int>
            {
                {Direction.North, Vector3Int.up},
                {Direction.West, Vector3Int.left},
                {Direction.South, Vector3Int.down},
                {Direction.East, Vector3Int.right}
            };
        }

        public static Vector3Int GetVector(Direction direction) => Directions[direction];
    }
}