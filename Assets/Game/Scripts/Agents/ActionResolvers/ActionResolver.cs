using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellularSpace;
using Game.Scripts.CellularSpace.CellObjects.Enums;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.CellularSpace.GridStep;
using Game.Scripts.General.FlexibleDataApi;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

namespace Game.Scripts.Agents.Interfaces
{
    public class ActionResolver : IActionResolver
    {
        private int _goalId;

        public void Init(IEnumerable<int> goalsIds)
        {
            _goalId = goalsIds.First();
        }

        public void ResolveAction(ActionBuffers actions, int agentId, IGridFacade gridFacade, Agent mlAgent)
        {
            var action = (MLAgentActions) actions.DiscreteActions[0];
            var doneAction = CommitAction(action, agentId, gridFacade);
            var path = gridFacade.PathFind(agentId, _goalId);
            var reward = RewardFromDoneAction(doneAction, path);
            mlAgent.AddReward(reward);
        }

        public float WinReward => 1;
        public float LoseReward => 0;

        private PerformanceParam CommitAction(MLAgentActions mlAgentAction, int agentId, IGridFacade gridFacade)
        {
            PerformanceParam performanceParam;
            switch (mlAgentAction)
            {
                case MLAgentActions.Forward:
                    performanceParam = CellObjectBaseActions.MoveForward;
                    break;
                case MLAgentActions.Back:
                    performanceParam = CellObjectBaseActions.MoveBack;
                    break;
                case MLAgentActions.Left:
                    performanceParam = CellObjectBaseActions.MoveLeft;
                    break;
                case MLAgentActions.Right:
                    performanceParam = CellObjectBaseActions.MoveRight;
                    break;
                case MLAgentActions.HitF:
                    performanceParam = CellAgentHitActions.HitF;
                    break;
                case MLAgentActions.HitB:
                    performanceParam = CellAgentHitActions.HitB;
                    break;
                case MLAgentActions.HitL:
                    performanceParam = CellAgentHitActions.HitL;
                    break;
                case MLAgentActions.HitR:
                    performanceParam = CellAgentHitActions.HitR;
                    break;
                case MLAgentActions.HitU:
                    performanceParam = CellAgentHitActions.HitU;
                    break;
                case MLAgentActions.HitD:
                    performanceParam = CellAgentHitActions.HitD;
                    break;
                case MLAgentActions.HitDF:
                    performanceParam = CellAgentHitActions.HitDF;
                    break;
                case MLAgentActions.HitDB:
                    performanceParam = CellAgentHitActions.HitDB;
                    break;
                case MLAgentActions.HitDL:
                    performanceParam = CellAgentHitActions.HitDL;
                    break;
                case MLAgentActions.HitDR:
                    performanceParam = CellAgentHitActions.HitDR;
                    break;
                case MLAgentActions.Stay:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mlAgentAction), mlAgentAction, null);
            }
            return !gridFacade.CommitAction(agentId, performanceParam) ? null : performanceParam;
        }

        private float _stayReward = -1e-4f;
        private float _stepReward = -5e-5f;
        private float _optimalStepReward = 0;
        
        private float RewardFromDoneAction(PerformanceParam doneParam, IEnumerable<(ICell, StepData)> path)
        {
            if (doneParam == null) return _stayReward;
            var pathArray = path.ToArray();
            if (pathArray.Length == 0) return _stepReward;
            
            var optimalDirection = pathArray[0].Item2.Direction;
            if (!CellObjectBaseAction.StepMove.Equals(doneParam.EnumActionType) &&
                 optimalDirection != doneParam.Vector3IntParam)
            {
                return _stepReward;
            }
            
            Debug.Log($"Optimal action on {optimalDirection}");
            return _optimalStepReward;
        }
    }
}