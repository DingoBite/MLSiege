using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.View.Agents;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.Fabrics
{
    public class MonoAgentFabric: IMonoFabric<MonoAgent, Agent>
    {
        private readonly IDictionary<AgentType, MonoAgent> _agentPrefabs;
        private readonly ITilemapLevelsGrid _tilemapLevelsGrid;

        public MonoAgentFabric([Inject] IDictionary<AgentType, MonoAgent> agentPrefabs, [Inject] ITilemapLevelsGrid tilemapLevelsGrid, Grid grid)
        {
            _agentPrefabs = agentPrefabs;
            _tilemapLevelsGrid = tilemapLevelsGrid;
            _tilemapLevelsGrid.Init(grid);
        }

        public MonoAgent Make(int id, int level, Vector3 position, Agent data)
        {
            var monoAgentPrefab = _agentPrefabs[data.Features.AgentType];
            var gameObjectMonoAgent = Object.Instantiate(monoAgentPrefab, position, Quaternion.identity, _tilemapLevelsGrid.GetLevel(level).transform);
            var monoAgent = gameObjectMonoAgent.GetComponent<MonoAgent>();
            monoAgent.Id = id;
            gameObjectMonoAgent.name = monoAgent.ToString();
            return monoAgent;
        }
    }
}