using System.Collections.Generic;
using Assets.Siege.Model.General.CellularSpace.Blocks;
using Assets.Siege.Model.General.CellularSpace.Blocks.BlockRealizations;
using Assets.Siege.Model.General.CellularSpace.Interfaces;
using Assets.Siege.Model.General.Repositories.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Siege.Model.General.CellularSpace
{
    public class BlockSpace: ICellularContext
    {
        [Inject] public readonly IRepository<AbstractBlock> BlockRepository;

        private readonly Dictionary<Vector3Int, int> _idByCoords;

        public BlockSpace()
        {
            _idByCoords = new Dictionary<Vector3Int, int>();
        }

        public int InsertBlock(Vector3Int coords, AbstractBlock block)
        {
            var id = BlockRepository.InsertCustomer(block);
            _idByCoords.Add(coords, id);
            return id;
        }

        public void DeleteCell(Vector3Int coords)
        {
            _idByCoords.Remove(coords);
            BlockRepository.DeleteCustomer(_idByCoords[coords]);
        }

        public bool TryGetBlock(Vector3Int coords, out AbstractBlock block)
        {
            return BlockRepository.TryGetCustomerById(_idByCoords[coords], out block);
        }

        public IEnumerable<AbstractBlock> GetCustomers() => BlockRepository.GetCustomers();

        public bool TryGetCustomerById(int id, out AbstractBlock customer) => BlockRepository.TryGetCustomerById(id, out customer);
    }
}