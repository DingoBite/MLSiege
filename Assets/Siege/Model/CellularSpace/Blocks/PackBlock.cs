using System;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.MonoBehaviors.Blocks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public class PackBlock: IToggle
    {
        public readonly Block Block;
        public readonly MonoBlock MonoBlock;

        public Vector3Int Coords { get; private set; }

        public PackBlock(Block block, MonoBlock monoBlock, Vector3Int coords)
        {
            if (block.Id != monoBlock.Id)
                throw new Exception("Block and MonoBlock Id is not equal");
            Block = block;
            MonoBlock = monoBlock;
            Coords = coords;
        }

        public int Id => Block.Id;

        public void SwapPosition(PackBlock packBlock)
        {
            var tempPos = packBlock.Coords;
            packBlock.Coords = Coords;
            Coords = tempPos;
        }

        public void Enable() => MonoBlock.gameObject.SetActive(true);

        public void Disable() => MonoBlock.gameObject.SetActive(false);

        public void Destroy() => Object.Destroy(MonoBlock.gameObject);
    }
}