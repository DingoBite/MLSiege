using System.Collections.Generic;
using System.Linq;
using Assets.Siege.CellularSpace.Agents.Enums;
using Assets.Siege.CellularSpace.General;
using Assets.Siege.CellularSpace.General.Interfaces;
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

            Container.Bind<IPrefabsByType<AgentType, MonoAgent>>().FromInstance(new PrefabsByType<AgentType, MonoAgent>(agentPrefabs))
                .AsSingle().NonLazy();
        }
    }
}