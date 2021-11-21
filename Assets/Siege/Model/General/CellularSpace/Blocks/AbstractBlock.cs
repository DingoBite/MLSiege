using Assets.Siege.Model.General.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.Model.ObjectFeatures;
using UnityEngine;

namespace Assets.Siege.Model.General.CellularSpace.Blocks
{
    public abstract class AbstractBlock
    {
        public readonly int Id;
        public readonly BlockType BlockType;
        public readonly AbstractFeatures Features;

        public AbstractBlock(Vector3Int coords, BlockType blockType, AbstractFeatures features, ICellularSpace cellularSpace)
        {
            BlockType = blockType;
            Features = features;
            Id = cellularSpace.InsertBlock(coords, this);
        }

        public abstract bool CommitAction(ICellularContext cellularContext);
    }
}