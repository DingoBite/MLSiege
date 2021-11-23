using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.CellularSpace.Repositories
{
    public class BlockSpace: IBlockSpace
    {
        private readonly IGridShaper _gridShaper;
        private readonly IBlockConverter _blockConverter;

        private readonly IRepository<OverallBlock> _overallBlocks;
        private readonly IIdRepository<Vector3Int> _ids;

        [Inject]
        public BlockSpace(
            Grid grid,
            IGridShaper gridShaper,
            IBlockConverter blockConverter,
            IRepository<OverallBlock> overallBlocks,
            IIdRepository<Vector3Int> ids)
        {
            _gridShaper = gridShaper;
            _blockConverter = blockConverter;
            _overallBlocks = overallBlocks;
            _ids = ids;
            _gridShaper.Shape(grid, this);
        }

        public int NextId => _overallBlocks.PeekId(); 

        public bool TryGetBlock(int id, out AbstractBlock block)
        {
            var successfulGet = _overallBlocks.TryGetCustomerById(id, out var overallBlock);
            block = !successfulGet ? null : overallBlock.Block;
            return successfulGet;
        }

        public bool TryGetBlock(Vector3Int coords, out AbstractBlock block)
        {
            var successfulGet = _overallBlocks.TryGetCustomerById(_ids[coords], out var overallBlock);
            block = !successfulGet ? null : overallBlock.Block;
            return successfulGet;
        }

        public int InsertBlock(Vector3Int coords, AbstractBlock block)
        {
            var id = _overallBlocks.InsertCustomer(_blockConverter.Convert(coords, block, NextId));
            _ids.Add(coords, id);
            return id;
        }

        public int InsertBlock(MonoBlock block)
        {
            var overallBlock = _blockConverter.Convert(block, NextId);
            var id = _overallBlocks.InsertCustomer(overallBlock);
            _ids.Add(overallBlock.Coords, id);
            return id;
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