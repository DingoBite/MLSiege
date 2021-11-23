using Assets.Siege.Model.General.Enums;

namespace Assets.Siege.MonoBehaviors.CellableObjects
{
    public readonly struct BlockInfo
    {
        public readonly BlockType BlockType;

        public BlockInfo(BlockType blockType)
        {
            BlockType = blockType;
        }
    }
}