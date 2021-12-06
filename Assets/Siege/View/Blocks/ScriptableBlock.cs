using Assets.Siege.Model.Agents;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.View.General;

namespace Assets.Siege.View.Blocks
{
    public abstract class ScriptableBlock: InfoScriptableObject<BlockInfo>
    {
        protected abstract bool CommitAction(SiegeAgent sender, Block committer, IBlockSpace blockSpace, ActionType actionType);
    }
}