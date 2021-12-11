using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.CoordsConverters.Interfaces;
using Assets.Siege.Model.BlockSpace.Fabrics.Interfaces;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using Assets.Siege.Model.BlockSpace.GridShapers.Interfaces;
using Assets.Siege.Model.BlockSpace.Repositories.Interfaces;
using Assets.Siege.View.General.MonoBehaviors;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.BlockSpace.Repositories
{
    public abstract class AbstractFrameSpace<TFrame, TInfo, TMono> : IBlockSpace<TFrame, TInfo, TMono> 
        where TMono: BlockSpaceMonoObject<TInfo>
    {
        protected readonly IFrameFabric<TFrame, TInfo, TMono> _frameFabric;
        protected readonly IRepository<TFrame> _frameRepository;
        protected readonly IIdRepository<Vector3Int> _idByCoords;
        private readonly IGridCoordsConverter _gridCoordsConverter;

        [Inject]
        protected AbstractFrameSpace(
            IFrameFabric<TFrame, TInfo, TMono> frameFabric,
            IRepository<TFrame> frameRepositoryRepository,
            IIdRepository<Vector3Int> idRepository,
            IGridShaper<TInfo, TMono> gridShaper,
            IGridCoordsConverter gridCoordsConverter)
        {
            _frameFabric = frameFabric;
            _frameRepository = frameRepositoryRepository;
            _idByCoords = idRepository;
            _gridCoordsConverter = gridCoordsConverter;

            _frameRepository.Clear();
            _idByCoords.Clear();
            var (minPoint, maxPoint) = gridShaper.Shape(this);

            FormingPoints = (Convert(minPoint), Convert(maxPoint));
        }

        public int PeekId => _frameRepository.PeekId;

        public (Vector3Int, Vector3Int) FormingPoints { get; }

        public Vector3 Convert(Vector3Int coords) => _gridCoordsConverter.Convert(coords);

        public Vector3Int Convert(Vector3 position) => _gridCoordsConverter.Convert(position);

        public abstract bool GetFrame(int id, out TFrame frame);

        public abstract bool GetFrame(Vector3Int coords, out TFrame frame);

        public abstract bool InsertBlock(Vector3Int coords, TInfo info, out int id);

        public abstract bool InsertBlock(Vector3Int coords, TInfo info);

        public abstract bool InsertBlock(TMono mono, out int id);

        public abstract bool InsertBlock(TMono mono);

        public abstract void SwapBlock(int id1, int id2);

        public abstract void SwapBlock(Vector3Int cords1, Vector3Int cords2);

        public abstract void MoveBlock(int id, Vector3Int newCoords);

        public abstract void MoveBlock(Vector3Int coords, Vector3Int newCoords);

        public abstract void DeleteBlock(int id);

        public abstract void DeleteBlock(Vector3Int coords);

        public void Clear()
        {
            _frameRepository.Clear();
            _idByCoords.Clear();
        }

        public IEnumerable<TFrame> GetBlocks() => _frameRepository.GetCustomers();
    }
}