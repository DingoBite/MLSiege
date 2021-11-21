using System.Collections.Generic;
using Assets.Siege.Model.General.CellularSpace.Blocks;
using Assets.Siege.Model.General.CellularSpace.CellularSpaceGenerator.Interfaces;
using Assets.Siege.Model.General.Repositories.Interfaces;
using Assets.Siege.MonoBehaviors.CellableObjects;
using Zenject;

namespace Assets.Siege.Model.General.CellularSpace.CellularSpaceGenerator
{
    public class CellularSpaceGenerator: IRepository<AbstractBlock>
    {
        [Inject] private readonly IRepository<CellableMono> _cellableMonoRepository;
        [Inject] private readonly IBlockFromMonoConverter _blockFromMonoConverter;

        private readonly Dictionary<int, AbstractBlock> _blocks;

        public CellularSpaceGenerator()
        {
            foreach (var cellableMono in _cellableMonoRepository.GetCustomers())
            {
                InsertCustomer(_blockFromMonoConverter.Convert(cellableMono));
            }
        }


        public IEnumerable<AbstractBlock> GetCustomers() => _blocks.Values;

        public bool TryGetCustomerById(int id, out AbstractBlock customer) => _blocks.TryGetValue(id, out customer);

        public int InsertCustomer(AbstractBlock customer)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCustomer(int id) => _blocks.Remove(id);

        public bool TryUpdateCustomer(int id, AbstractBlock newCustomer)
        {
            if (!_blocks.ContainsKey(id))
                return false;
            _blocks[id] = newCustomer;
            return true;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}