using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.General.CellObjects;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Repositories.Interfaces
{
    public interface IFrameSpaceInfo<TFrame>
    {
        public int PeekId { get; }
        public (Vector3Int, Vector3Int) FormingPoints { get; }
        public Vector3 Convert(Vector3Int coords);
        public Vector3Int Convert(Vector3 position);
        public bool GetFrame(int id, out TFrame frame);
        public bool GetFrame(Vector3Int coords, out TFrame frame);
        public IEnumerable<TFrame> GetBlocks();
    }
}