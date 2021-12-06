using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Fabrics.Interfaces;
using Assets.Siege.Model.CellularSpace.GridShapers.Interfaces;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Repositories
{
    public class BlockSpace: IBlockSpace
    {
        private readonly IPackBlockFabric _packBlockFabric;
        private readonly IRepository<PackBlock> _packBlocks;
        private readonly IIdRepository<Vector3Int> _idByCoords;

        [Inject]
        public BlockSpace(
            IPackBlockFabric packBlockFabric,
            IGameObjectGrid gameObjectGrid,
            IGridShaper gridShaper)
        {
            _packBlockFabric = packBlockFabric;
            _packBlocks = new PackBlockRepository();
            _idByCoords = new Vector3IntIdRepository();
            gridShaper.Shape(gameObjectGrid, this);
        }

        private int PeekId => _packBlocks.PeekId();

        public bool GetPackBlock(int id, out Block block)
        {
            var successfulGet = _packBlocks.TryGetCustomerById(id, out var packBlock);
            block = !successfulGet ? null : packBlock;
            return successfulGet;
        }

        public bool GetPackBlock(Vector3Int coords, out Block block)
        {
            var successfulGet = _packBlocks.TryGetCustomerById(_idByCoords[coords], out var packBlock);
            block = !successfulGet ? null : packBlock;
            return successfulGet;
        }

        public bool GetBlock(int id, out Block block)
        {
            var successfulGet = GetPackBlock(id, out var packBlock);
            block = !successfulGet ? null : packBlock.Block;
            return successfulGet;
        }

        public bool GetBlock(Vector3Int coords, out Block block)
        {
            var successfulGet = GetPackBlock(coords, out var packBlock);
            block = !successfulGet ? null : packBlock.Block;
            return successfulGet;
        }

        public bool InsertBlock(Vector3Int coords, BlockInfo blockInfo, out int id)
        {
            if (_idByCoords.ContainsKey(coords))
            {
                id = -1;
                return false;
            }
            id = _packBlocks.InsertCustomer(_packBlockFabric.Make(PeekId, coords, blockInfo));
            _idByCoords.Add(coords, id);
            return true;
        }

        public bool InsertBlock(Vector3Int coords, BlockInfo blockInfo)
        {
            if (_idByCoords.ContainsKey(coords))
                return false;
            
            var id = _packBlocks.InsertCustomer(_packBlockFabric.Make(PeekId, coords, blockInfo));
            _idByCoords.Add(coords, id);
            return true;
        }

        public bool InsertBlock(MonoBlock block, out int id)
        {
            var overallBlock = _packBlockFabric.Make(PeekId, block);
            if (_idByCoords.ContainsKey(overallBlock.Coords))
            {
                id = -1;
                return false;
            }
            id = _packBlocks.InsertCustomer(overallBlock);
            _idByCoords.Add(overallBlock.Coords, id);
            return true;
        }

        public bool InsertBlock(MonoBlock block)
        {
            var overallBlock = _packBlockFabric.Make(PeekId, block);
            if (_idByCoords.ContainsKey(overallBlock.Coords))
                return false;
            var id = _packBlocks.InsertCustomer(overallBlock);
            _idByCoords.Add(overallBlock.Coords, id);
            return true;
        }

        public void SwapBlock(int id1, int id2)
        {
            var successfulGet1 = _packBlocks.TryGetCustomerById(id1, out var block1);
            var successfulGet2 = _packBlocks.TryGetCustomerById(id2, out var block2);
            if (successfulGet1 && successfulGet2)
                block1.SwapPosition(block2);
            else
                throw new NullReferenceException("Swap block null reference exception");
        }

        public void SwapBlock(Vector3Int cords1, Vector3Int cords2)
        {
            var successfulGet1 = _packBlocks.TryGetCustomerById(_idByCoords[cords1], out var block1);
            var successfulGet2 = _packBlocks.TryGetCustomerById(_idByCoords[cords2], out var block2);
            if (successfulGet1 && successfulGet2)
                block1.SwapPosition(block2);
            else
                throw new NullReferenceException("Swap block null reference exception");
        }

        public void DeleteBlock(Vector3Int coords)
        {
            if (!_idByCoords.ContainsKey(coords))
                return;
            _packBlocks.DeleteCustomer(_idByCoords[coords]);
            _idByCoords.Remove(coords);
        }

        public void DeleteBlock(int id)
        {
            if (!_packBlocks.TryGetCustomerById(id, out var block))
                return;
            _packBlocks.DeleteCustomer(id);
            _idByCoords.Remove(block.Coords);
        }

        public void Clear()
        {
            _packBlocks.Clear();
            _idByCoords.Clear();
        }

        public IEnumerable<Block> GetPackBlocks()
        {
            return _packBlocks.GetCustomers();
        }

        public IEnumerable<Block> GetBlocks()
        {
            return GetPackBlocks().Select(b => b.Block);
        }
    }
}