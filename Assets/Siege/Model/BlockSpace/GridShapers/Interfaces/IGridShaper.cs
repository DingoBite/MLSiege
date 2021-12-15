using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.GridShapers.Interfaces
{
    public interface IGridShaper<out TInfo, out TMono> where TMono : FrameSpaceMonoObject<TInfo>
    {
        public (Vector3, Vector3) Shape(IFrameSpaceDataController<TInfo, TMono> frameSpace);
    }
}