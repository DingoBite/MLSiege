using Assets.Siege.Model.General.CellularSpace.Blocks;
using UnityEngine;

namespace Assets.Siege.Model.General.CellularSpace.Interfaces
{
    public interface ICellularSpace
    {
        public int InsertBlock(Vector3Int coords, AbstractBlock block);
        public void DeleteCell(Vector3Int coords);
        public bool TryGetBlock(Vector3Int coords, out AbstractBlock block);
    }
}