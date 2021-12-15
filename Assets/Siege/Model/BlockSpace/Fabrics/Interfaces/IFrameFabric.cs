using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Fabrics.Interfaces
{
    public interface IFrameFabric<TFrame, in TInfo, in TMono> where TMono: FrameSpaceMonoObject<TInfo>
    {
        public TFrame Make(Vector3Int coords, TInfo info, IFrameSpaceContext<TFrame> space);
        public TFrame Make(TMono mono, IFrameSpaceContext<TFrame> space);
    }
}