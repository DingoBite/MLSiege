using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Fabrics.Interfaces
{
    public interface IFrameFabric<TFrame, TInfo, TMono> where TMono: BlockSpaceMonoObject<TInfo>
    {
        public TFrame Make(Vector3Int coords, TInfo info, IBlockSpace<TFrame, TInfo, TMono> space);
        public TFrame Make(TMono mono, IBlockSpace<TFrame, TInfo, TMono> space);
    }
}