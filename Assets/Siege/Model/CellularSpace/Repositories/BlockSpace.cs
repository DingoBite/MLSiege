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
        private readonly IBlockCreator _blockCreator;
        private readonly IRepository<OverallBlock> _overallBlocks;
        private readonly IIdRepository<Vector3Int> _ids;

        [Inject]
        public BlockSpace(
            IGameObjectGrid gameObjectGrid,
            IGridShaper gridShaper,
            IBlockCreator blockCreator,
            IRepository<OverallBlock> overallBlocks,
            IIdRepository<Vector3Int> ids)
        {
            _blockCreator = blockCreator;
            _overallBlocks = overallBlocks;
            _ids = ids;
            gridShaper.Shape(gameObjectGrid, this);
        }

        public int PeekId => _overallBlocks.PeekId(); 

        public bool GetBlock(int id, out AbstractBlock block)
        {
            var successfulGet = _overallBlocks.TryGetCustomerById(id, out var overallBlock);
            block = !successfulGet ? null : overallBlock.Block;
            return successfulGet;
        }

        public bool GetBlock(Vector3Int coords, out AbstractBlock block)
        {
            var successfulGet = _overallBlocks.TryGetCustomerById(_ids[coords], out var overallBlock);
            block = !successfulGet ? null : overallBlock.Block;
            return successfulGet;
        }

        public bool InsertBlock(Vector3Int coords, BlockFeatures blockFeatures, out int id)
        {
            if (_ids.ContainsKey(coords))
            {
                id = -1;
                return false;
            }
            id = _overallBlocks.InsertCustomer(_blockCreator.Create(PeekId, coords, blockFeatures));
            _ids.Add(coords, id);
            return true;
        }

        public bool InsertBlock(Vector3Int coords, BlockFeatures blockFeatures)
        {
            if (_ids.ContainsKey(coords))
                return false;
            
            var id = _overallBlocks.InsertCustomer(_blockCreator.Create(PeekId, coords, blockFeatures));
            _ids.Add(coords, id);
            return true;
        }

        public bool InsertBlock(MonoBlock block, out int id)
        {
            var overallBlock = _blockCreator.Create(PeekId, block);
            if (_ids.ContainsKey(overallBlock.Coords))
            {
                id = -1;
                return false;
            }
            id = _overallBlocks.InsertCustomer(overallBlock);
            _ids.Add(overallBlock.Coords, id);
            return true;
        }

        public bool InsertBlock(MonoBlock block)
        {
            var overallBlock = _blockCreator.Create(PeekId, block);
            if (_ids.ContainsKey(overallBlock.Coords))
                return false;
            var id = _overallBlocks.InsertCustomer(overallBlock);
            _ids.Add(overallBlock.Coords, id);
            return true;
        }

        public void SwapBlock(int id1, int id2)
        {
            var successfulGet1 = _overallBlocks.TryGetCustomerById(id1, out var block1);
            var successfulGet2 = _overallBlocks.TryGetCustomerById(id2, out var block2);
            if (successfulGet1 && successfulGet2)
                block1.SwapPosition(block2);
            else
                throw new NullReferenceException("Swap block null reference exception");
        }

        public void SwapBlock(Vector3Int cords1, Vector3Int cords2)
        {
            var successfulGet1 = _overallBlocks.TryGetCustomerById(_ids[cords1], out var block1);
            var successfulGet2 = _overallBlocks.TryGetCustomerById(_ids[cords2], out var block2);
            if (successfulGet1 && successfulGet2)
                block1.SwapPosition(block2);
            else
                throw new NullReferenceException("Swap block null reference exception");
        }

        public void DeleteBlock(Vector3Int coords)
        {
            if (!_ids.ContainsKey(coords))
                return;
            _overallBlocks.DeleteCustomer(_ids[coords]);
            _ids.Remove(coords);
        }

        public void Clear()
        {
            _overallBlocks.Clear();
            _ids.Clear();
        }

        public IEnumerable<AbstractBlock> GetCustomers()
        {
            return _overallBlocks.GetCustomers().Select(ob => ob.Block);
        }
    }
}