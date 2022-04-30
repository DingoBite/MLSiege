using System;
using UnityEngine;

namespace Game.Scripts.General.FlexibleDataApi
{
    public class PerformanceParam
    {
        public readonly Enum EnumActionType;
        
        public readonly int? IntParam;
        public readonly bool? Flag;
        public readonly Vector3Int? Vector3IntParam;
        public readonly FlexibleData FlexibleData;

        public PerformanceParam(Enum enumActionType,
            int? intParam = null, bool? flag = null,
            Vector3Int? vector3IntParam = null,
            FlexibleData flexibleData = null)
        {
            EnumActionType = enumActionType;
            IntParam = intParam;
            Flag = flag;
            Vector3IntParam = vector3IntParam;
            FlexibleData = flexibleData;
        }
    }
}