using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace;
using Unity.MLAgents.Actuators;

namespace Game.Scripts.SiegeML.ActionResolvers
{
    public interface IActionResolver
    {
        void Init(IEnumerable<int> goalsIds, int agentsNearGoalToWin, int agentsCount, Action winAction, Action loseAction);
        void SetLearnEpisodesInfo(Func<int> completeEpisodesGetter, int maxEpisodes);
        float ResolveAction(ActionBuffers actions, int agentId, IGridFacade gridFacade);
        float ResolveWin();
        float ResolveLose();
        void AddAgentNearGoal(int agentNumber);
        bool IsEnoughAgentsNearGoal();
    }
}