using System;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.View.MLAgents;
using Object = UnityEngine.Object;

namespace Assets.Siege.Model.Agents
{
    public class PackAgent: IToggle, IDisposable
    {
        public readonly SiegeAgent SiegeAgent;
        private readonly MonoAgent _monoAgent;

        public PackAgent(SiegeAgent siegeAgent, MonoAgent monoAgent)
        {
            SiegeAgent = siegeAgent;
            _monoAgent = monoAgent;
        }


        public void Enable() => _monoAgent.gameObject.SetActive(true);

        public void Disable() => _monoAgent.gameObject.SetActive(false);

        public void Dispose() => Object.Destroy(_monoAgent);

        ~PackAgent() => Dispose();
    }
}