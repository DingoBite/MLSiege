using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.View.General.MonoBehaviors;

namespace Assets.Siege.View.Blocks
{
    public class MonoBlock : CellularSpaceMonoObject<BlockInfo>
    {
        public override string ToString()
        {
            return $"{_actableScriptableObject.GetInfo().BlockData.BlockType}";
        }
    }
}
