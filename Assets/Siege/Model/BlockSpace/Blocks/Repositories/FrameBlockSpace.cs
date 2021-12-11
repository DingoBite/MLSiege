using System;
using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories;
using Assets.Siege.View.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.Blocks.Repositories
{
    public class FrameBlockSpace: AbstractFrameSpace<FrameBlock, BlockInfo, MonoBlock>
    {
        [Inject]
        public FrameBlockSpace(
            IFrameFabric<FrameBlock, BlockInfo, MonoBlock> frameFabric,
            IRepository<FrameBlock> frameRepositoryRepository,
            IIdRepository<Vector3Int> idRepository,
            IGridShaper<BlockInfo, MonoBlock> gridShaper,
            IGridCoordsConverter gridCoordsConverter) 
            : base(frameFabric, frameRepositoryRepository, idRepository, gridShaper, gridCoordsConverter)
        {
        }

        public override bool GetFrame(int id, out FrameBlock frame)
        {
            var successfulGet = _frameRepository.TryGetCustomerById(id, out var packBlock);
            frame = !successfulGet ? null : packBlock;
            return successfulGet;
        }

        public override bool GetFrame(Vector3Int coords, out FrameBlock frame)
        {
            if (!_idByCoords.ContainsKey(coords))
            {
                frame = null;
                return false;
            }
            var successfulGet = _frameRepository.TryGetCustomerById(_idByCoords[coords], out var packBlock);
            frame = !successfulGet ? null : packBlock;
            return successfulGet;
        }

        public override bool InsertBlock(Vector3Int coords, BlockInfo info, out int id)
        {
            if (_idByCoords.ContainsKey(coords))
            {
                id = -1;
                return false;
            }
            id = _frameRepository.InsertCustomer(_frameFabric.Make(coords, info, this));
            _idByCoords[coords] = id;
            return true;
        }

        public override bool InsertBlock(Vector3Int coords, BlockInfo info)
        {
            if (_idByCoords.ContainsKey(coords))
                return false;
            
            var id = _frameRepository.InsertCustomer(_frameFabric.Make(coords, info, this));
            _idByCoords[coords] = id;
            return true;
        }

        public override bool InsertBlock(MonoBlock mono, out int id)
        {
            var packBlock = _frameFabric.Make(mono, this);
            if (_idByCoords.ContainsKey(packBlock.Coords))
            {
                id = -1;
                return false;
            }
            id = _frameRepository.InsertCustomer(packBlock);
            _idByCoords[packBlock.Coords] = id;
            return true;
        }

        public override bool InsertBlock(MonoBlock mono)
        {
            var packBlock = _frameFabric.Make(mono, this);
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
                throw new NullReferenceException("Swap frame by id null reference exception");
        }

        public override void SwapBlock(Vector3Int cords1, Vector3Int cords2)
        {
            var successfulGet1 = _frameRepository.TryGetCustomerById(_idByCoords[cords1], out var block1);
            var successfulGet2 = _frameRepository.TryGetCustomerById(_idByCoords[cords2], out var block2);
            if (successfulGet1 && successfulGet2)
                block1.SwapPosition(block2);
            else
                throw new NullReferenceException("Swap frame by coords null reference exception");
        }

        public override void MoveBlock(int id, Vector3Int newCoords)
        {
            if (_idByCoords.ContainsKey(newCoords))
                throw new Exception($"Block space is already have frame on new Coords = {newCoords}");

            var successfulGet = _frameRepository.TryGetCustomerById(id, out var block);
            if (!successfulGet)
                throw new KeyNotFoundException($"Block space doesn't contains id = {id}");

            _idByCoords[newCoords] = id;
            block.Coords = newCoords;
        }

        public override void MoveBlock(Vector3Int coords, Vector3Int newCoords)
        {
            if (!_idByCoords.ContainsKey(coords))
                throw new KeyNotFoundException($"Block space doesn't contains frame on {coords}");
            MoveBlock(_idByCoords[coords], newCoords);
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