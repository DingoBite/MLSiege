using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.Agents.Enums;
using Assets.Siege.CellularSpace.Fabrics.Interfaces;
using Assets.Siege.CellularSpace.General.Interfaces;
using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.View.Agents;
using UnityEngine;
using Zenject;

namespace Assets.Siege.CellularSpace.Fabrics
{
    public class MonoAgentFabric: IMonoFabric<MonoAgent, Agent>
    {
        private readonly IPrefabsByType<AgentType, MonoAgent> _agentPrefabs;
        private ITilemapLevelsGrid<MonoAgent> _tilemapLevelsGrid;

        [Inject]
        public MonoAgentFabric(IPrefabsByType<AgentType, MonoAgent> blockPrefabs)
        {
            _agentPrefabs = blockPrefabs;
        }

        public void Init(ITilemapLevelsGrid<MonoAgent> tilemapLevelsGrid)
        {
            _tilemapLevelsGrid = tilemapLevelsGrid;
        }

        public MonoAgent Make(int id, int level, Vector3 position, Agent data)
        {
            var monoAgentPrefab = _agentPrefabs[data.Features.AgentType];
            return _tilemapLevelsGrid.PutIntoLevel(level, monoAgentPrefab, position, id);
        }
    }
}