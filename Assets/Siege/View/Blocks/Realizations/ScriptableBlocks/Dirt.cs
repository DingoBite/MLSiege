﻿using Assets.Siege.CellularSpace.Blocks;
using Assets.Siege.CellularSpace.Blocks.Enums;
using Assets.Siege.View.Blocks.Realizations.ScriptableBlockTypes;
using UnityEngine;

namespace Assets.Siege.View.Blocks.Realizations.ScriptableBlocks
{
    [CreateAssetMenu(fileName = "Dirt", menuName = "ScriptableObjects/Blocks/DestructibleBlock/Dirt")]
    public class Dirt: DestructibleBlock
    {
        public override BlockInfo GetInfo() 
            => new BlockInfo(new BlockData(BlockType.Dirt, BlockSolidity.Solid), CommitAction, CommitAction);
    }
}