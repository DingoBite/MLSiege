﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.General.StaticUtils
{
    public static class Vector3IntUtils
    {
        public static IEnumerable<Vector3Int> GetPointsInRect(Vector3Int minFormingPoint, Vector3Int maxFormingPoint)
        {
            var xSign = Math.Sign(maxFormingPoint.x);
            var ySign = Math.Sign(maxFormingPoint.y);
            var zSign = Math.Sign(maxFormingPoint.z);
            var offset = Vector3Int.zero;
            for (var i = 0; i != maxFormingPoint.x; i += xSign)
            {
                offset.x = i;
                for (var j = 0; j != maxFormingPoint.y; j += ySign)
                {
                    offset.y = j;
                    for (var k = 0; k != maxFormingPoint.z; k += zSign)
                    {
                        offset.z = k;
                        var point = minFormingPoint + offset;
                        yield return point;
                    }   
                }
            }
        }
    }
}