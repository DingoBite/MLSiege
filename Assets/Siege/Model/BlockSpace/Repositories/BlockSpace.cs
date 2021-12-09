using System;
using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.Blocks;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.General.Interfaces;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.Repositories
{
    public class BlockSpace: AbstractBlockSpace<FrameBlock, BlockInfo, MonoBlock>
    {
        [Inject]
        public BlockSpace(
            IFrameFabric<FrameBlock, BlockInfo, MonoBlock> frameFabric,
            IRepository<FrameBlock> frameRepositoryRepository,
            IIdRepository<Vector3Int> idRepository,
            IGameObjectGrid gameObjectGrid,
            IGridShaper<BlockInfo, MonoBlock> gridShaper,
            IGridCoordsConverter gridCoordsConverter) 
            : base(frameFabric, frameRepositoryRepository, idRepository, gameObjectGrid, gridShaper, gridCoordsConverter)
        {
        }

        public override bool GetFrame(int id, out FrameBlock block)
        {
            var successfulGet = _frameRepository.TryGetCustomerById(id, out var packBlock);
            block = !successfulGet ? null : packBlock;
            return successfulGet;
        }

        public override bool GetFrame(Vector3Int coords, out FrameBlock block)
        {
            var successfulGet = _frameRepository.TryGetCustomerById(_idByCoords[coords], out var packBlock);
            block = !successfulGet ? null : packBlock;
            return successfulGet;
        }

        public override bool InsertBlock(Vector3Int coords, BlockInfo blockInfo, out int id)
        {
            if (_idByCoords.ContainsKey(coords))
            {
                id = -1;
                return false;
            }
            id = _frameRepository.InsertCustomer(_frameFabric.Make(coords, blockInfo, this));
            _idByCoords[coords] = id;
            return true;
        }

        public override bool InsertBlock(Vector3Int coords, BlockInfo blockInfo)
        {
            if (_idByCoords.ContainsKey(coords))
                return false;
            
            var id = _frameRepository.InsertCustomer(_frameFabric.Make(coords, blockInfo, this));
            _idByCoords[coords] = id;
            return true;
        }

        public override bool InsertBlock(MonoBlock block, out int id)
        {
            var packBlock = _frameFabric.Make(block, this);
            if (_idByCoords.ContainsKey(packBlock.Coords))
            {
                id = -1;
                return false;
            }
            id = _frameRepository.InsertCustomer(packBlock);
            _idByCoords[packBlock.Coords] = id;
            return true;
        }

        public override bool InsertBlock(MonoBlock block)
        {
            var packBlock = _frameFabric.Make(block, this);
            if (_idByCoords.ContainsKey(packBlock.Coords))
                return false;
            var id = _frameRepository.InsertCustomer(packBlock);
            _idByCoords[packBlock.Coords] = id;
            return true;
        }

        public override void SwapBlock(int id1, int id2)
        {
            var successfulGet1 = _frameRepository.TryGetCustomerById(id1, out var block1);
            var successfulGet2 = _frameRepository.TryGetCustomerById(id2, out var block2);
            if (successfulGet1 && successfulGet2)
                block1.SwapPosition(block2);
            else
                throw new NullReferenceException("Swap block by id null reference exception");
        }

        public override void SwapBlock(Vector3Int cords1, Vector3Int cords2)
        {
            var successfulGet1 = _frameRepository.TryGetCustomerById(_idByCoords[cords1], out var block1);
            var successfulGet2 = _frameRepository.TryGetCustomerById(_idByCoords[cords2], out var block2);
            if (successfulGet1 && successfulGet2)
                block1.SwapPosition(block2);
            else
                throw new NullReferenceException("Swap block by coords null reference exception");
        }

        public override void MoveBlock(int id, Vector3Int newCoords)
        {
            if (_idByCoords.ContainsKey(newCoords))
                throw new Exception($"Block space is already have block on new Coords = {newCoords}");

            var successfulGet = _frameRepository.TryGetCustomerById(id, out var block);
            if (!successfulGet)
                throw new KeyNotFoundException($"Block space doesn't contains id = {id}");

            _idByCoords[newCoords] = id;
            block.Coords = newCoords;
        }

        public override void MoveBlock(Vector3Int blockCoords, Vector3Int newCoords)
        {
            if (!_idByCoords.ContainsKey(blockCoords))
                throw new KeyNotFoundException($"Block space doesn't contains block on {blockCoords}");
            MoveBlock(_idByCoords[blockCoords], newCoords);
        }

        public override void DeleteBlock(int id)
        {
            if (!_frameRepository.TryGetCustomerById(id, out var block))
                return;
            _frameRepository.DeleteCustomer(id);
            _idByCoords.Remove(block.Coords);
        }

        public override void DeleteBlock(Vector3Int coords)
        {
            if (!_idByCoords.ContainsKey(coords))
                return;
            _frameRepository.DeleteCustomer(_idByCoords[coords]);
            _idByCoords.Remove(coords);
        }
    }
}