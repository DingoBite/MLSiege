using Assets.Siege.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.CellularSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.CellularSpace.Fabrics.Interfaces
{
    public interface IFrameFabric<TFrame, in TInfo, TMono> where TMono: CellularSpaceMonoObject<TInfo>
    {
        public void Init(ITilemapLevelsGrid<TMono> tilemapLevelsGrid);
        public TFrame Make(Vector3Int coords, TInfo info, IFrameSpaceContext<TFrame> space);
        public TFrame Make(TMono monoAgent, IFrameSpaceContext<TFrame> space);
    }
}