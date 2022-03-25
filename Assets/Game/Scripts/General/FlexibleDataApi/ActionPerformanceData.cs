using System;

namespace Game.Scripts.General.FlexibleDataApi
{
    public class ActionPerformanceData<TActionType> : FlexibleData where TActionType : Enum
    {
        public readonly TActionType ActionType;
        
        public ActionPerformanceData(TActionType actionType)
        {
            ActionType = actionType;
        }
    }
}