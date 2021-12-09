using System.Collections.Generic;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Repositories.Interfaces
{
    public interface IFrameSpaceContext<TFrame>
    {
        public IEnumerable<TFrame> GetBlocks();
        public bool GetFrame(int id, out TFrame block);
        public bool GetFrame(Vector3Int coords, out TFrame block);
        public int PeekId();
        public Vector3 Convert(Vector3Int coords);
        public Vector3Int Convert(Vector3 position);
    }
}