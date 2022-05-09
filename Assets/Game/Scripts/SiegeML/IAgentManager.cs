using System;
using System.Collections.Generic;
using Game.Scripts.CellularSpace;
using Game.Scripts.SiegeML.ActionResolvers;
using Game.Scripts.SiegeML.ObservationsCollectors;
using Unity.MLAgents.Actuators;

namespace Game.Scripts.SiegeML
{
    public interface IAgentManager
    {
        void Init(IGridFacade gridFacade, IObservationsCollector observationsCollector, IActionResolver actionResolver,
            int agentsNearGoalToWin, Action winAction, Action loseAction);

        void SetLearnEpisodesInfo(Func<int> completeEpisodesGetter, int maxEpisodes);
        
        void ReInit();
        List<float> CollectObservations();
        bool IsWinState();
        float ResolveAction(ActionBuffers actions);
        float ResolveWin();
        float ResolveLose();
    }
}