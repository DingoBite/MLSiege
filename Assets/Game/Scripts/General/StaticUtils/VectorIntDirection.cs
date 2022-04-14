using System.Collections.Generic;
using Game.Scripts.CellularSpace.CellStorages.CellObjects.Enums;
using Game.Scripts.General.Enums;
using UnityEngine;

namespace Game.Scripts.General.StaticUtils
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
        
        public static Vector3Int? VectorFromDirection(CellObjectBaseAction direction)
        {
            return direction switch
            {
                CellObjectBaseAction.MoveUp => Vector3Int.up,
                CellObjectBaseAction.MoveLeft => Vector3Int.left,
                CellObjectBaseAction.MoveRight => Vector3Int.right,
                CellObjectBaseAction.MoveForward => Vector3Int.forward,
                CellObjectBaseAction.MoveBack => Vector3Int.back,
                CellObjectBaseAction.MoveDown => Vector3Int.down,
                _ => null
            };
        }
    }
}