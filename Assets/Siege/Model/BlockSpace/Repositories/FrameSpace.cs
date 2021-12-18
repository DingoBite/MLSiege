using System;
using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.General.CellObjects;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.Repositories
{
    public sealed class FrameSpace<TFrame, TInfo, TMono> : IFrameSpace<TFrame, TInfo, TMono> 
        where TFrame: ISpaceLocated
        where TMono: FrameSpaceMonoObject<TInfo>
    {
        private readonly IFrameFabric<TFrame, TInfo, TMono> _frameFabric;
        private readonly IRepository<TFrame> _frameRepository;
        private readonly IIdRepository<Vector3Int> _idByCoords;
        private readonly IGridCoordsConverter _gridCoordsConverter;

        private FrameSpace(
            [Inject] IFrameFabric<TFrame, TInfo, TMono> frameFabric,
            [Inject] IRepository<TFrame> frameRepositoryRepository,
            [Inject] IIdRepository<Vector3Int> idRepository,
            [Inject] IGridShaper<TInfo, TMono> gridShaper,
            IGridCoordsConverter gridCoordsConverter)
        {
            _frameFabric = frameFabric;
            _frameRepository = frameRepositoryRepository;
            _idByCoords = idRepository;
            _gridCoordsConverter = gridCoordsConverter;

            var (minPoint, maxPoint) = gridShaper.Shape(this);

            FormingPoints = (Convert(minPoint), Convert(maxPoint));
        }

        public int PeekId => _frameRepository.PeekId;

        public (Vector3Int, Vector3Int) FormingPoints { get; }

        public Vector3 Convert(Vector3Int coords) => _gridCoordsConverter.Convert(coords);

        public Vector3Int Convert(Vector3 position) => _gridCoordsConverter.Convert(position);

        public bool GetFrame(int id, out TFrame frame)
        {
            var successfulGet = _frameRepository.TryGetCustomerById(id, out var frameAgent);
            frame = !successfulGet ? default : frameAgent;
            return successfulGet;
        }

        public bool GetFrame(Vector3Int coords, out TFrame frame)
        {
            if (!_idByCoords.ContainsKey(coords))
            {
                frame = default;
                return false;
            }
            var successfulGet = _frameRepository.TryGetCustomerById(_idByCoords[coords], out var frameAgent);
            frame = !successfulGet ? default : frameAgent;
            return successfulGet;
        }

        public bool Insert(Vector3Int coords, TInfo info, out int id)
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

        public bool Insert(Vector3Int coords, TInfo info)
        {
            if (_idByCoords.ContainsKey(coords))
                return false;

            var id = _frameRepository.InsertCustomer(_frameFabric.Make(coords, info, this));
            _idByCoords[coords] = id;
            return true;
        }

        public bool Insert(TMono mono, out int id)
        {
            var frameAgent = _frameFabric.Make(mono, this);
            if (_idByCoords.ContainsKey(frameAgent.Coords))
            {
                id = -1;
                return false;
            }
            id = _frameRepository.InsertCustomer(frameAgent);
            _idByCoords[frameAgent.Coords] = id;
            return true;
        }

        public bool Insert(TMono mono)
        {
            var frameAgent = _frameFabric.Make(mono, this);
            if (_idByCoords.ContainsKey(frameAgent.Coords))
                return false;
            var id = _frameRepository.InsertCustomer(frameAgent);
            _idByCoords[frameAgent.Coords] = id;
            return true;
        }

        public void Swap(int id1, int id2)
        {
            var successfulGet1 = _frameRepository.TryGetCustomerById(id1, out var agent1);
            var successfulGet2 = _frameRepository.TryGetCustomerById(id2, out var agent2);
            if (successfulGet1 && successfulGet2)
                agent1.SwapPosition(agent2);
            else
                throw new NullReferenceException("Swap frame by id null reference exception");
        }

        public void Swap(Vector3Int cords1, Vector3Int cords2)
        {
            var successfulGet1 = _frameRepository.TryGetCustomerById(_idByCoords[cords1], out var agent1);
            var successfulGet2 = _frameRepository.TryGetCustomerById(_idByCoords[cords2], out var agent2);
            if (successfulGet1 && successfulGet2)
                agent1.SwapPosition(agent2);
            else
                throw new NullReferenceException("Swap frame by coords null reference exception");
        }

        public void MoveTo(Vector3Int newCoords, int id)
        {
            if (_idByCoords.ContainsKey(newCoords))
                throw new Exception($"Agent space is already have frame on new Coords = {newCoords}");

            var successfulGet = _frameRepository.TryGetCustomerById(id, out var frame);
            if (!successfulGet)
                throw new KeyNotFoundException($"Agent space doesn't contains id = {id}");

            _idByCoords[newCoords] = id;
            _idByCoords.Remove(frame.Coords);
            frame.UnsafeCoordsChange(newCoords);
        }

        public void MoveTo(Vector3 newPosition, int id) => MoveTo(Convert(newPosition), id);

        public void MoveTo(Vector3Int newCoords, Vector3Int coords)
        {
            if (!_idByCoords.ContainsKey(coords))
                throw new KeyNotFoundException($"Agent space doesn't contains frame on {coords}");
            MoveTo(newCoords, _idByCoords[coords]);
        }

        public void MoveTo(Vector3 newPosition, Vector3Int coords) => MoveTo(Convert(newPosition), coords);

        public void Delete(int id)
        {
            if (!_frameRepository.TryGetCustomerById(id, out var frame))
                return;
            _frameRepository.DeleteCustomer(id);
            _idByCoords.Remove(frame.Coords);
        }

        public void Delete(Vector3Int coords)
        {
            if (!_idByCoords.ContainsKey(coords))
                return;
            _frameRepository.DeleteCustomer(_idByCoords[coords]);
            _idByCoords.Remove(coords);
        }

        public void Clear()
        {
            _frameRepository.Clear();
            _idByCoords.Clear();
        }

        public IEnumerable<TFrame> GetBlocks() => _frameRepository.GetCustomers();
    }
}