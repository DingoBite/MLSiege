using System;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public class OverallBlock: IToggle
    {
        public readonly AbstractBlock Block;
        public readonly MonoBlock MonoBlock;
        public Vector3Int Coords { get; private set; }

        public OverallBlock(AbstractBlock block, MonoBlock monoBlock, Vector3Int coords)
        {
            if (block.Id != monoBlock.Id)
                throw new Exception("Block and MonoBlock Id is not equal");
            Block = block;
            MonoBlock = monoBlock;
            Coords = coords;
        }

        public int Id => Block.Id;

        public void SwapPosition(OverallBlock overallBlock)
        {
            var tempPos = overallBlock.Coords;
            overallBlock.Coords = Coords;
            Coords = tempPos;
        }

        public void Enable() => MonoBlock.gameObject.SetActive(true);

        public void Disable() => MonoBlock.gameObject.SetActive(false);

        public void Destroy() => Object.Destroy(MonoBlock.gameObject);
    }
}