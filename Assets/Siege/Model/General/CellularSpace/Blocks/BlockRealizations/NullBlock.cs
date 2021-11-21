using System;
using Assets.Siege.Model.General.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using UnityEngine;

namespace Assets.Siege.Model.General.CellularSpace.Blocks.BlockRealizations
{
    public class NullBlock: AbstractBlock
    {
        public NullBlock(Vector3Int coords, ICellularSpace cellularSpace) : base(coords, BlockType.Null, null, cellularSpace)
        {
        }

        public override bool CommitAction(ICellularContext cellularContext)
        {
            throw new Exception("Try to commit action at NullBlock");
        }
    }
}