using System;

namespace Game.Scripts.General.FlexibleDataApi
{
    public class PerformanceParams
    {
        public readonly Enum RawActionType;
        
        public FlexibleData FlexibleData => _flexibleData ??= new FlexibleData();
        private FlexibleData _flexibleData;
        
        public PerformanceParams(Enum rawActionType)
        {
            RawActionType = rawActionType;
        }
    }
}