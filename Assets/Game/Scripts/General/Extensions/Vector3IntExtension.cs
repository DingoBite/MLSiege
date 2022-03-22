using System;
using UnityEngine;

namespace Game.Scripts.General.Extensions
{
    public static class Vector3IntExtension
    {
        public static bool IsInRadiusRect(this Vector3Int radiusVector, Vector3Int vectorToCheck)
        {
            var absoluteRadiusVector = new Vector3Int(Math.Abs(radiusVector.x), Math.Abs(radiusVector.y), Math.Abs(radiusVector.z));
            var absoluteCheckedVector = new Vector3Int(Math.Abs(vectorToCheck.x), Math.Abs(vectorToCheck.y), Math.Abs(vectorToCheck.z));
            return absoluteCheckedVector.x < absoluteRadiusVector.x &&
                   absoluteCheckedVector.y < absoluteRadiusVector.y &&
                   absoluteCheckedVector.z < absoluteRadiusVector.z;
        }
    }
}