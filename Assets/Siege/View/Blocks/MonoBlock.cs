using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.View.General.MonoBehaviors;

namespace Assets.Siege.View.Blocks
{
    public class MonoBlock : BlockSpaceMonoObject<BlockInfo>
    {
        public override string ToString()
        {
            return $"{_actableScriptableObject.GetInfo().BlockData.BlockType}";
        }
    }
}
