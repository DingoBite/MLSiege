using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.CellObjects.Enums;
using Game.Scripts.CellObjects.Enums.Agent;
using Game.Scripts.CellularSpace;
using Game.Scripts.CellularSpace.CellStorages.Interfaces;
using Game.Scripts.General.FlexibleDataApi;
using Game.Scripts.PathFind;
using Unity.MLAgents.Actuators;
using UnityEngine;

namespace Game.Scripts.SiegeML.ActionResolvers
{
    public class ActionResolver : IActionResolver
    {
        private int _goalId;
        private Action _winAction;
        private Action _loseAction;

        private const float WinReward = 0.3f;
        private const float LoseReward = -0.5f;
        private const float EndEpisodeReward = 0.2f;

        private float StayReward = -1e-4f;
        private float StepReward = -1e-5f;
        

        private HashSet<int> _nearGoalAgents;
        private int _agentsNearGoal;
        private int _agentsNearGoalToWin;
        private float _endEpisodeRewardOffset;
        private int _agentCount;
        
        private Func<int> _completeEpisodesGetter;

        private int _maxEpisodes;

        public void Init(IEnumerable<int> goalsIds, int agentsNearGoalToWin, int agentsCount, Action winAction, Action loseAction)
        {
            _nearGoalAgents = new HashSet<int>();
            _agentsNearGoalToWin = Mathf.Clamp(agentsNearGoalToWin, 0, agentsCount);
            _goalId = goalsIds.First();
            _winAction = winAction;
            _loseAction = loseAction;
            _agentCount = agentsCount;
        }

        private void ReInit()
        {
            _endEpisodeRewardOffset = 0;
            _agentsNearGoal = 0;
            _nearGoalAgents.Clear();
        }
        
        private float OptimalStepReward
        {
            get
            {
                if (_completeEpisodesGetter != null) return StepReward * 0.1f *
                                                            ((float) (_completeEpisodesGetter() + 1) / (_maxEpisodes + 1));
                return StepReward;
            }
        }

        private float HardOptimalStepReward
        {
            get
            {
                if (_completeEpisodesGetter != null) return StepReward * 0.8f *
                                                            ((float) (_completeEpisodesGetter() + 1) / (_maxEpisodes + 1));
                return StepReward;
            }
        }
        
        public void SetLearnEpisodesInfo(Func<int> completeEpisodesGetter, int maxEpisodes)
        {
            _completeEpisodesGetter = completeEpisodesGetter;
            _maxEpisodes = maxEpisodes;
        }

        public float ResolveAction(ActionBuffers actions, int agentId, IGridFacade gridFacade)
        {
            var action = (MLAgentActions) actions.DiscreteActions[0];
            var path = gridFacade.PathFind(agentId, _goalId);
            var doneAction = CommitAction(action, agentId, gridFacade);
            var reward = RewardFromDoneAction(agentId, gridFacade, doneAction, path);
            return reward;
        }

        public float ResolveWin()
        {
            _winAction.Invoke();
            var reward = WinReward + _endEpisodeRewardOffset;
            ReInit();
            return reward;
        }

        public float ResolveLose()
        {
            _loseAction.Invoke();
            var reward = LoseReward + _endEpisodeRewardOffset;
            ReInit();
            return reward;
        }

        public void AddAgentNearGoal(int agentNumber)
        {
            if (_nearGoalAgents.Contains(agentNumber)) return;
            _nearGoalAgents.Add(agentNumber);
            _endEpisodeRewardOffset += EndEpisodeReward / _agentCount;
            _agentsNearGoal++;
        }

        public bool IsEnoughAgentsNearGoal() => _agentsNearGoal >= _agentsNearGoalToWin;

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

        private float RewardFromDoneAction(int agentId, IGridFacade gridFacade, PerformanceParam doneParam, IEnumerable<(ICell, StepData)> path)
        {   
            var pathArray = path.ToArray();
            if (pathArray.Length == 0 && doneParam == null) return StayReward;
            var optimalDirection = pathArray[0].Item2.Direction;
            optimalDirection.y = 0;
            //var optimalMoveDirection = new Vector3Int(optimalDirection.x, 0, optimalDirection.z);
            if (doneParam == null)
            {
                var stepParam = new ActPerformanceParam<CellObjectBaseAction>(CellObjectBaseAction.StepMove,
                    vector3IntParam: optimalDirection);
                if (gridFacade.CommitAction(agentId, stepParam))
                    return OptimalStepReward;
                
                var hitDirectionParam = new ActPerformanceParam<CellAgentAction>(CellAgentAction.Hit,
                    vector3IntParam: optimalDirection);
                if (gridFacade.CommitAction(agentId, hitDirectionParam))
                    return OptimalStepReward;
                
                var hitDownDirectionParam = new ActPerformanceParam<CellAgentAction>(CellAgentAction.Hit,
                    vector3IntParam: optimalDirection + Vector3Int.down);
                if (gridFacade.CommitAction(agentId, hitDownDirectionParam))
                    return OptimalStepReward;
                
                return StayReward;
            }
            
            if (CellObjectBaseAction.StepMove.Equals(doneParam.EnumActionType) && optimalDirection == doneParam.Vector3IntParam)
                return OptimalStepReward;
            return StepReward;
        }
    }
}