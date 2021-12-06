using Assets.Siege.Model.Agents;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Enums;
using Assets.Siege.View.Blocks.ScriptableBlockTypes;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations
{
    [CreateAssetMenu(fileName = "Dirt", menuName = "ScriptableObjects/Blocks/DestructibleBlock/Dirt")]
    public class Dirt: DestructibleBlock
    {
        public override BlockInfo GetInfo() => new BlockInfo(BlockType.Dirt, BlockSolidity.Solid, CommitAction);

        protected override bool CommitAction(SiegeAgent sender, Block committer, IBlockSpace blockSpace, ActionType actionType)
        {
            switch (actionType)
            {
                case ActionType.Destroy:
                {
                    blockSpace.DeleteBlock(committer.Id);
                    return true;
                }
                case ActionType.Take:
                    return false;
                case ActionType.Push:
                    return false;
                case ActionType.Use:
                    return false;
                case ActionType.Hit:
                    return false;
                default:
                    return false;
            }
        }
    }
}