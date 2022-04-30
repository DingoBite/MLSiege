using System;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellObjects.Enums.Agent;
using Game.Scripts.CellularSpace.CellObjects.Enums.Block;
using UnityEngine;

namespace Game.Scripts.General.FlexibleDataApi
{
    public class ActPerformanceParam<TActionType> : PerformanceParam where TActionType : Enum
    {
        public readonly TActionType ActionType;

        public ActPerformanceParam(TActionType actionType) : base(actionType)
        {
            ActionType = actionType;
        }

        public ActPerformanceParam(TActionType actionType, int? intParam = null, 
            bool? flag = null, 
            Vector3Int? vector3IntParam = null,
            FlexibleData flexibleData = null) 
            : base(actionType, intParam, flag, vector3IntParam, flexibleData)
        {
            ActionType = actionType;
        }
    }
    
    public static class CellObjectBaseActions
    {
        public static readonly ActPerformanceParam<CellObjectBaseAction> Select = 
            new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.Select);
        public static readonly ActPerformanceParam<CellObjectBaseAction> Unselect = 
            new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.Unselect);
        public static readonly ActPerformanceParam<CellObjectBaseAction> MoveUp = 
            new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.StepMove, vector3IntParam: Vector3Int.up);
        public static readonly ActPerformanceParam<CellObjectBaseAction> MoveLeft = 
            new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.StepMove, vector3IntParam: Vector3Int.left);
        public static readonly ActPerformanceParam<CellObjectBaseAction> MoveRight = 
            new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.StepMove, vector3IntParam: Vector3Int.right);
        public static readonly ActPerformanceParam<CellObjectBaseAction> MoveForward = 
            new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.StepMove, vector3IntParam: Vector3Int.forward);
        public static readonly ActPerformanceParam<CellObjectBaseAction> MoveBack = 
            new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.StepMove, vector3IntParam: Vector3Int.back);
        public static readonly ActPerformanceParam<CellObjectBaseAction> MoveDown = 
            new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.StepMove, vector3IntParam: Vector3Int.down);
        public static readonly ActPerformanceParam<CellObjectBaseAction> Dispose = 
            new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.Dispose);
    }
    
    public static class CellBlockViewActions
    {
        public static readonly ActPerformanceParam<CellBlockViewAction> Select = 
            new ActPerformanceParam<CellBlockViewAction>(CellBlockViewAction.Select);
        public static readonly ActPerformanceParam<CellBlockViewAction> Unselect = 
            new ActPerformanceParam<CellBlockViewAction>(CellBlockViewAction.Unselect);
        public static readonly ActPerformanceParam<CellBlockViewAction> Dispose = 
            new ActPerformanceParam<CellBlockViewAction>(CellBlockViewAction.Dispose);
        public static readonly ActPerformanceParam<CellBlockViewAction> Error = 
            new ActPerformanceParam<CellBlockViewAction>(CellBlockViewAction.Error);
    }
    
    public static class CellAgentViewActions
    {
        public static readonly ActPerformanceParam<CellAgentViewAction> Select = 
            new ActPerformanceParam<CellAgentViewAction>(CellAgentViewAction.Select);
        public static readonly ActPerformanceParam<CellAgentViewAction> Unselect = 
            new ActPerformanceParam<CellAgentViewAction>(CellAgentViewAction.Unselect);
        public static readonly ActPerformanceParam<CellAgentViewAction> Dispose = 
            new ActPerformanceParam<CellAgentViewAction>(CellAgentViewAction.Dispose);
        public static readonly ActPerformanceParam<CellAgentViewAction> Error = 
            new ActPerformanceParam<CellAgentViewAction>(CellAgentViewAction.Error);
    }
}