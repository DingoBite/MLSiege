using System.Threading.Tasks;
using Game.Scripts.Agents.Interfaces;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

namespace Game.Scripts.Agents.Learning
{
    public class SiegeAgents : AgentWithEndGame
    {
        private IAgentManager _agentManager;
        private bool _isFirstRun = true;

        public void Init(IAgentManager agentManager)
        {
            _agentManager = agentManager;
        }

        public override void OnEpisodeBegin()
        {
            RequestAction();
            if (_isFirstRun)
            {
                _isFirstRun = false;
                Debug.Log("First Run");
                return;
            }
            Debug.Log("New Episode");
        }

        public override async void CollectObservations(VectorSensor sensor)
        {
            await Task.Run(() => _agentManager.CollectObservations(sensor));
        }

        public override async void OnActionReceived(ActionBuffers actions)
        {
            await Task.Run(() => _agentManager.ResolveAction(actions, this));
            if (StepCount == MaxStep)
            {
                Lose();
                return;
            }
            RequestDecision();
        }

        public override void Win()
        {
            _agentManager.ResolveWin(this);
            EndEpisode();
        }

        public override void Lose()
        {
            _agentManager.ResolveLose(this);
            EndEpisode();
        }
    }
    
    public abstract class AgentWithEndGame : Agent
    {
        public abstract void Win();
        public abstract void Lose();
    }
}