using Assets.Siege.CellularSpace.Agents;
using Assets.Siege.CellularSpace.Fabrics.Interfaces;
using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.Agents;
using UnityEngine;
using Zenject;

namespace Assets.Siege.CellularSpace.Fabrics
{
    public class FrameAgentFabric : IFrameFabric<FrameAgent, AgentInfo, MonoAgent>
    {
        private readonly IMonoFabric<MonoAgent, Agent> _monoFabric;

        [Inject]
        public FrameAgentFabric(IMonoFabric<MonoAgent, Agent> monoFabric)
        {
            _monoFabric = monoFabric;
        }

        public void Init(ITilemapLevelsGrid<MonoAgent> tilemapLevelsGrid) => _monoFabric.Init(tilemapLevelsGrid);

        public FrameAgent Make(Vector3Int coords, AgentInfo info, IFrameSpaceContext<FrameAgent> space)
        {
            var id = space.PeekId;
            var agent = new Agent(info);
            var position = space.Convert(coords);
            var monoAgent = _monoFabric.Make(id, coords.y, position, agent);
            return new FrameAgent(space, agent, monoAgent);
        }

        public FrameAgent Make(MonoAgent monoAgent, IFrameSpaceContext<FrameAgent> space)
        {
            monoAgent.Id = space.PeekId;
            monoAgent.name = monoAgent.ToString();
            var agent = new Agent(monoAgent.GetInfo());
            return new FrameAgent(space, agent, monoAgent);
        }
    }
}