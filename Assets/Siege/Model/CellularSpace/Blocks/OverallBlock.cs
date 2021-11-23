using System;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public class OverallBlock
    {
        public readonly AbstractBlock Block;
        public readonly MonoBlock MonoBlock;
        public Vector3Int Coords { get; private set; }

        public OverallBlock(AbstractBlock block, MonoBlock monoBlockBlock, Vector3Int coords)
        {
            if (block.Id != monoBlockBlock.Id)
                throw new Exception("Block and _monoBlockBlock Id is not equal");
            Block = block;
            MonoBlock = monoBlockBlock;
            Coords = coords;
        }

        public int Id => Block.Id;

        public void SwapPosition(OverallBlock overallBlock)
        {
            var tempPos = overallBlock.Coords;
            overallBlock.Coords = Coords;
            Coords = tempPos;
        }
    }
}