﻿using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.Blocks.Enums;
using Assets.Siege.View.Blocks.Realizations.ScriptableBlockTypes;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations.ScriptableBlocks
{
    [CreateAssetMenu(fileName = "Stone", menuName = "ScriptableObjects/Blocks/DestructibleBlock/Stone")]
    public class Stone: DestructibleBlock
    {
        public override BlockInfo GetInfo() =>
            new BlockInfo(new BlockData(BlockType.Stone, BlockSolidity.Solid), CommitAction, CommitAction);
    }
}