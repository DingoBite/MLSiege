using System.Collections.Generic;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.General.Interfaces;

namespace Assets.Siege.Model.CellularSpace.Repositories
{
    public class PackBlockRepository: IRepository<PackBlock>
    {
        private readonly Dictionary<int, PackBlock> _packBlocks;
        private int _lastId;
        private readonly Stack<int> _availableId;

        public PackBlockRepository()
        {
            _packBlocks = new Dictionary<int, PackBlock>();
            _lastId = 0;
            _availableId = new Stack<int>();
        }

        public int PeekId() => _availableId.Count >= 1 ? _availableId.Peek() : _lastId;

        public IEnumerable<PackBlock> GetCustomers()
        {
            return _packBlocks.Values;
        }

        public bool TryGetCustomerById(int id, out PackBlock customer)
        {
            return _packBlocks.TryGetValue(id, out customer);
        }

        public int InsertCustomer(PackBlock customer)
        {
            if (_availableId.Count >= 1)
            {
                var id = _availableId.Pop();
                _packBlocks[id] = customer;
                return id;
            }
            _packBlocks[_lastId++] = customer;
            return _lastId - 1;
        }

        public void DeleteCustomer(int id)
        {
            if (!_packBlocks.ContainsKey(id))
                return;
            
            _availableId.Push(id);
            _packBlocks[id].Destroy();
            _packBlocks.Remove(id);
        }

        public bool TryUpdateCustomer(int id, PackBlock newCustomer)
        {
            if (!_packBlocks.ContainsKey(id))
                return false;
            _packBlocks[id] = newCustomer;
            return true;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            foreach (var overallBlock in _packBlocks)
            {
                overallBlock.Value.Destroy();
            }

            _lastId = 0;
            _availableId.Clear();
            _packBlocks.Clear();
        }
    }
}