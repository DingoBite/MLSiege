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
        where TFrame : IMovable
        where TMono : FrameSpaceMonoObject<TInfo>
    {
        private readonly IFrameFabric<TFrame, TInfo, TMono> _frameFabric;
        private readonly IRepository<TFrame> _frameRepository;
        private readonly IRepository<Vector3Int, int> _idByCoords;
        private readonly IRepository<int, Vector3Int> _coordsById;
        private readonly IGridShaper<TInfo, TMono> _gridShaper;
        private readonly IGridCoordsConverter _gridCoordsConverter;

        [Inject]
        private FrameSpace(IFrameFabric<TFrame, TInfo, TMono> frameFabric, IRepository<TFrame> frameRepository,
            IRepository<Vector3Int, int> idByCoordsRepository, IRepository<int, Vector3Int> coordsByIdRepository,
            IGridShaper<TInfo, TMono> gridShaper, IGridCoordsConverter gridCoordsConverter)
        {
            _frameFabric = frameFabric;
            _frameRepository = frameRepository;
            _coordsById = coordsByIdRepository;
            _idByCoords = idByCoordsRepository;
            _gridShaper = gridShaper;
            _gridCoordsConverter = gridCoordsConverter;
        }

        public void Init(ITilemapLevelsGrid<TMono> tilemapLevelsGrid)
        {
            _frameRepository.Clear();
            _coordsById.Clear();
            _idByCoords.Clear();

            _frameFabric.Init(tilemapLevelsGrid);
            _gridShaper.Init(tilemapLevelsGrid);
            _gridCoordsConverter.Init(tilemapLevelsGrid.GetCellSize());

            var (minPoint, maxPoint) = _gridShaper.Shape(this);
            FormingPoints = (Convert(minPoint), Convert(maxPoint));
        }

        public int this[Vector3Int coords] => _idByCoords[coords];

        public Vector3Int this[int id] => _coordsById[id];

        public int PeekId => _frameRepository.PeekId;

        public (Vector3Int, Vector3Int) FormingPoints { get; private set; }

        private void UpdateKeys(Vector3Int coords, int id)
        {
            _idByCoords[coords] = id;
            _coordsById[id] = coords;
        }

        public Vector3 Convert(Vector3Int coords) => _gridCoordsConverter.Convert(coords);

        public Vector3Int Convert(Vector3 position) => _gridCoordsConverter.Convert(position);

        public bool GetFrame(int id, out TFrame frame)
        {
            var successfulGet = _frameRepository.TryGetCustomer(id, out var tempFrame);
            frame = !successfulGet ? default : tempFrame;
            return successfulGet;
        }

        public bool GetFrame(Vector3Int coords, out TFrame frame)
        {
            if (!_idByCoords.ContainsKey(coords))
            {
                frame = default;
                return false;
            }
            var successfulGet = _frameRepository.TryGetCustomer(_idByCoords[coords], out var frameAgent);
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
            UpdateKeys(coords, id);
            return true;
        }

        public bool Insert(Vector3Int coords, TInfo info)
        {
            if (_idByCoords.ContainsKey(coords))
                return false;

            var id = _frameRepository.InsertCustomer(_frameFabric.Make(coords, info, this));
            _idByCoords[coords] = id;
            _coordsById[id] = coords;
            return true;
        }

        public bool Insert(TMono mono, out int id)
        {
            var frameAgent = _frameFabric.Make(mono, this);
            var coords = Convert(mono.transform.position);

            if (_idByCoords.ContainsKey(coords))
            {
                id = -1;
                return false;
            }

            id = _frameRepository.InsertCustomer(frameAgent);
            UpdateKeys(coords, id);
            return true;
        }

        public bool Insert(TMono mono)
        {
            var frameAgent = _frameFabric.Make(mono, this);
            var coords = Convert(mono.transform.position);

            if (_idByCoords.ContainsKey(coords))
                return false;

            var id = _frameRepository.InsertCustomer(frameAgent);
            UpdateKeys(coords, id);
            return true;
        }

        public void Swap(int id1, int id2)
        {
            var successfulGet1 = _coordsById.TryGetCustomer(id1, out var coords1);
            var successfulGet2 = _coordsById.TryGetCustomer(id2, out var coords2);
            if (successfulGet1 && successfulGet2)
            {
                _frameRepository.TryGetCustomer(id1, out var frame1);
                frame1.Move(Convert(coords2), () => UpdateKeys(coords2, id1));
                _frameRepository.TryGetCustomer(id2, out var frame2);
                frame2.Move(Convert(coords1), () => UpdateKeys(coords1, id2));
            }
            else
                throw new NullReferenceException("Swap frame by id null reference exception");
        }

        public void Swap(Vector3Int coords1, Vector3Int coords2)
        {
            var successfulGet1 = _idByCoords.TryGetCustomer(coords1, out var id1);
            var successfulGet2 = _idByCoords.TryGetCustomer(coords2, out var id2);
            if (successfulGet1 && successfulGet2)
            {
                _frameRepository.TryGetCustomer(id1, out var frame1);
                frame1.Move(Convert(coords2), () => UpdateKeys(coords2, id1));
                _frameRepository.TryGetCustomer(id2, out var frame2);
                frame2.Move(Convert(coords1), () => UpdateKeys(coords1, id2));
            }
            else
                throw new NullReferenceException("Swap frame by coords null reference exception");
        }

        public void MoveTo(Vector3Int newCoords, int id)
        {
            if (_idByCoords.ContainsKey(newCoords))
                throw new Exception($"Agent space is already have frame on new Coords = {newCoords}");

            var successfulGet = _coordsById.TryGetCustomer(id, out var coords);
            if (!successfulGet)
                throw new KeyNotFoundException($"Agent space doesn't contains id = {id}");

            _frameRepository.TryGetCustomer(id, out var frame);
            UpdateKeys(newCoords, id);
            _idByCoords.Remove(coords);
            frame.Move(Convert(coords));
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
            if (!_coordsById.TryGetCustomer(id, out var frame))
                return;
            var coords = _coordsById[id];
            _frameRepository.DeleteCustomer(id);
            _idByCoords.Remove(coords);
            _coordsById.Remove(id);
        }

        public void Delete(Vector3Int coords)
        {
            if (!_idByCoords.ContainsKey(coords))
                return;
            Delete(_idByCoords[coords]);
        }

        public void Clear()
        {
            _coordsById.Clear();
            _idByCoords.Clear();
        }

        public IEnumerable<TFrame> GetFrames() => _frameRepository.GetCustomers();
    }
}