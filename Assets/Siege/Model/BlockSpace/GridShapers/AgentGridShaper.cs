using Assets.Siege.Model.BlockSpace.Agents;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.Agents;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.GridShapers
{
    public class AgentGridShaper: IGridShaper<AgentInfo, MonoAgent>
    {
        private readonly ITilemapLevelsGrid _tilemapLevelsGrid;

        public AgentGridShaper(Grid grid, [Inject] ITilemapLevelsGrid tilemapLevelsGrid)
        {
            _tilemapLevelsGrid = tilemapLevelsGrid;
            _tilemapLevelsGrid.Init(grid);
        }

        public (Vector3, Vector3) Shape(IBlockSpaceController<AgentInfo, MonoAgent> blockSpace)
        {
            var minX = float.MaxValue;
            var minY = float.MaxValue;
            var minZ = float.MaxValue;
            var maxX = float.MinValue;
            var maxY = float.MinValue;
            var maxZ = float.MinValue;

            foreach (var tilemap in _tilemapLevelsGrid.GetLevels())
            {
                foreach (Transform agent in tilemap.transform)
                {
                    var isBlock = agent.TryGetComponent(out MonoAgent monoAgent);
                    if (!isBlock) continue;
                    blockSpace.InsertBlock(monoAgent);
                    var pos = agent.position;
                    if (pos.x < minX) minX = pos.x;
                    if (pos.y < minY) minY = pos.y;
                    if (pos.z < minZ) minZ = pos.z;
                    if (pos.x > maxX) maxX = pos.x;
                    if (pos.y > maxY) maxY = pos.y;
                    if (pos.z > maxZ) maxZ = pos.z;
                }
            }

            return (new Vector3(minX, minY, minZ), new Vector3(maxX, maxY, maxZ));
        }
    }
}