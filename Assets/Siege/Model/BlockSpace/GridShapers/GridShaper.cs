using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.GridShapers
{
    public class GridShaper<TInfo, TMono> : IGridShaper<TInfo, TMono> where TMono : FrameSpaceMonoObject<TInfo>
    {
        private readonly ITilemapLevelsGrid<TMono> _tilemapLevelsGrid;

        protected GridShaper(ITilemapLevelsGrid<TMono> tilemapLevelsGrid)
        {
            _tilemapLevelsGrid = tilemapLevelsGrid;
        }

        public (Vector3, Vector3) Shape(IFrameSpaceDataController<TInfo, TMono> frameSpace)
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
                    var isBlock = agent.TryGetComponent(out TMono monoAgent);
                    if (!isBlock) continue;
                    frameSpace.Insert(monoAgent);
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