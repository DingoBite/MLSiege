using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.Agents.Enums;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.View.Agents;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.Fabrics
{
    public class MonoAgentFabric: IMonoFabric<MonoAgent, Agent>
    {
        private readonly IPrefabsByType<AgentType, MonoAgent> _agentPrefabs;
        private readonly ITilemapLevelsGrid<MonoAgent> _tilemapLevelsGrid;

        [Inject]
        public MonoAgentFabric(IPrefabsByType<AgentType, MonoAgent> agentPrefabs, ITilemapLevelsGrid<MonoAgent> tilemapLevelsGrid)
        {
            _agentPrefabs = agentPrefabs;
            _tilemapLevelsGrid = tilemapLevelsGrid;
        }

        public MonoAgent Make(int id, int level, Vector3 position, Agent data)
        {
            var monoAgentPrefab = _agentPrefabs[data.Features.AgentType];
            return _tilemapLevelsGrid.PutIntoLevel(level, monoAgentPrefab, position, id);
        }
    }
}