using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace Game.Scripts.SiegeML.Learning
{
    public class SiegeAgents : Agent
    {
        [SerializeField] [Range(0, 1e7f)] private int _optimalStepRewardMaxEpisodes;
        
        private IAgentManager _agentManager;
        private bool _isFirstRun = true;

        public void Init(IAgentManager agentManager)
        {
            _agentManager = agentManager;
            _agentManager.SetLearnEpisodesInfo(() => CompletedEpisodes, _optimalStepRewardMaxEpisodes);
        }

        public override void OnEpisodeBegin()
        {
            if (_isFirstRun)
            {
                _isFirstRun = false;
                Debug.Log("First Run");
                RequestDecision();
                return;
            }
            Debug.Log("New Episode");
            RequestDecision();
        }

        public override void CollectObservations(VectorSensor sensor)
        {
            var observations = _agentManager.CollectObservations();
            sensor.AddObservation(observations);
        }

        public override void OnActionReceived(ActionBuffers actionBuffers)
        {
            var reward = _agentManager.ResolveAction(actionBuffers);
            AddReward(reward);
            if (_agentManager.IsWinState())
            {
                Win();
                return;
            }
            if (StepCount >= MaxStep)
            {
                Lose();
                return;
            }
            RequestDecision();
        }

        private void Win()
        {
            var reward = _agentManager.ResolveWin();
            AddReward(reward);
            EndEpisode();
        }

        private void Lose()
        {
            var reward = _agentManager.ResolveLose();
            AddReward(reward);
            EndEpisode();
        }
    }
}