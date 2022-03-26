using System;

namespace Game.Scripts.General.FlexibleDataApi
{
    public class ActionPerformanceParams<TActionType> : PerformanceParams where TActionType : Enum
    {
        public readonly TActionType ActionType;

        public ActionPerformanceParams(TActionType actionType) : base(actionType)
        {
            ActionType = actionType;
        }
    }
}