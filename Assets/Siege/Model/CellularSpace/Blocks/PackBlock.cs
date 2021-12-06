using System;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Siege.Model.CellularSpace.Blocks
{
    public class PackBlock: IToggle, IDisposable
    {
        public readonly Block Block;
        private readonly MonoBlock _monoBlock;

        public Vector3Int Coords { get; private set; }

        public PackBlock(Block block, MonoBlock monoBlock, Vector3Int coords)
        {
            if (block.Id != monoBlock.Id)
                throw new Exception("Block and MonoBlock Id is not equal");
            Block = block;
            _monoBlock = monoBlock;
            Coords = coords;
        }

        public int Id => Block.Id;

        public void SwapPosition(PackBlock packBlock)
        {
            var tempPos = packBlock.Coords;
            packBlock.Coords = Coords;
            Coords = tempPos;
        }

        public void Enable() => _monoBlock.gameObject.SetActive(true);

        public void Disable() => _monoBlock.gameObject.SetActive(false);

        public void Dispose() => Object.Destroy(_monoBlock.gameObject);

        ~PackBlock() => Dispose();
    }
}