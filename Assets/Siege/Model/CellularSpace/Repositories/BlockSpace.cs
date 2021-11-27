using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.Model.ObjectFeatures.Blocks;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Repositories
{
    public class BlockSpace: IBlockSpace
    {
        private readonly IPackBlockFabric _packBlockFabric;
        private readonly IRepository<PackBlock> _packBlocks;
        private readonly IIdRepository<Vector3Int> _ids;

        [Inject]
        public BlockSpace(
            IGameObjectGrid gameObjectGrid,
            IGridShaper gridShaper,
            IPackBlockFabric packBlockFabric,
            IRepository<PackBlock> packBlocks,
            IIdRepository<Vector3Int> ids)
        {
            _packBlockFabric = packBlockFabric;
            _packBlocks = packBlocks;
            _ids = ids;
            gridShaper.Shape(gameObjectGrid, this);
        }

        private int PeekId => _packBlocks.PeekId();

        public bool GetPackBlock(int id, out PackBlock block)
        {
            var successfulGet = _packBlocks.TryGetCustomerById(id, out var overallBlock);
            block = !successfulGet ? null : overallBlock;
            return successfulGet;
        }

        public bool GetBlock(int id, out Block block)
        {
            var successfulGet = GetPackBlock(id, out PackBlock packBlock);
            block = !successfulGet ? null : packBlock.Block;
            return successfulGet;
        }

        public bool GetPackBlock(Vector3Int coords, out PackBlock block)
        {
            var successfulGet = _packBlocks.TryGetCustomerById(_ids[coords], out var overallBlock);
            block = !successfulGet ? null : overallBlock;
            return successfulGet;
        }

        public bool GetBlock(Vector3Int coords, out Block block)
        {
            var successfulGet = GetPackBlock(coords, out PackBlock packBlock);
            block = !successfulGet ? null : packBlock.Block;
            return successfulGet;
        }

        public bool InsertBlock(Vector3Int coords, BlockFeatures blockFeatures, out int id)
        {
            if (_ids.ContainsKey(coords))
            {
                id = -1;
                return false;
            }
            id = _packBlocks.InsertCustomer(_packBlockFabric.Make(PeekId, coords, blockFeatures));
            _ids.Add(coords, id);
            return true;
        }

        public bool InsertBlock(Vector3Int coords, BlockFeatures blockFeatures)
        {
            if (_ids.ContainsKey(coords))
                return false;
            
            var id = _packBlocks.InsertCustomer(_packBlockFabric.Make(PeekId, coords, blockFeatures));
            _ids.Add(coords, id);
            return true;
        }

        public bool InsertBlock(MonoBlock block, out int id)
        {
            var overallBlock = _packBlockFabric.Make(PeekId, block);
            if (_ids.ContainsKey(overallBlock.Coords))
            {
                id = -1;
                return false;
            }
            id = _packBlocks.InsertCustomer(overallBlock);
            _ids.Add(overallBlock.Coords, id);
            return true;
        }

        public bool InsertBlock(MonoBlock block)
        {
            var overallBlock = _packBlockFabric.Make(PeekId, block);
            if (_ids.ContainsKey(overallBlock.Coords))
                return false;
            var id = _packBlocks.InsertCustomer(overallBlock);
            _ids.Add(overallBlock.Coords, id);
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
            var successfulGet1 = _packBlocks.TryGetCustomerById(_ids[cords1], out var block1);
            var successfulGet2 = _packBlocks.TryGetCustomerById(_ids[cords2], out var block2);
            if (successfulGet1 && successfulGet2)
                block1.SwapPosition(block2);
            else
                throw new NullReferenceException("Swap block null reference exception");
        }

        public void DeleteBlock(Vector3Int coords)
        {
            if (!_ids.ContainsKey(coords))
                return;
            _packBlocks.DeleteCustomer(_ids[coords]);
            _ids.Remove(coords);
        }

        public void Clear()
        {
            _packBlocks.Clear();
            _ids.Clear();
        }

        public IEnumerable<PackBlock> GetPackBlocks()
        {
            return _packBlocks.GetCustomers();
        }

        public IEnumerable<Block> GetBlocks()
        {
            return GetPackBlocks().Select(b => b.Block);
        }
    }
}