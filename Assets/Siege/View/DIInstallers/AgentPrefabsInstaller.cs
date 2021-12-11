using System.Collections.Generic;
using System.Linq;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.View.Agents;
using UnityEngine;
using Zenject;

namespace Assets.Siege.View.DIInstallers
{
    public class AgentPrefabsInstaller : MonoInstaller
    {
        [SerializeField] private List<MonoAgent> _agentPrefabs;

        public override void InstallBindings()
        {
            var agentPrefabs = _agentPrefabs
                .ToDictionary(mb => mb.GetInfo().AgentType, mb => mb);

            Container.Bind<IDictionary<AgentType, MonoAgent>>().FromInstance(agentPrefabs).AsSingle();
        }
    }
}