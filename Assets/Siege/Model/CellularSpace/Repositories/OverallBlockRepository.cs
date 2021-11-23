using System.Collections.Generic;
using Assets.Siege.Model.CellularSpace.Blocks;
using Assets.Siege.Model.General.Interfaces;

namespace Assets.Siege.Model.CellularSpace.Repositories
{
    public class OverallBlockRepository: IRepository<OverallBlock>
    {
        private readonly Dictionary<int, OverallBlock> _overallBlocks;
        private int _lastId;
        private readonly Stack<int> _availableId;

        public OverallBlockRepository()
        {
            _overallBlocks = new Dictionary<int, OverallBlock>();
            _lastId = 0;
            _availableId = new Stack<int>();
        }

        public int PeekId() => _availableId.Count >= 1 ? _availableId.Peek() : _lastId;

        public IEnumerable<OverallBlock> GetCustomers()
        {
            return _overallBlocks.Values;
        }

        public bool TryGetCustomerById(int id, out OverallBlock customer)
        {
            return _overallBlocks.TryGetValue(id, out customer);
        }

        public int InsertCustomer(OverallBlock customer)
        {
            if (_availableId.Count >= 1)
            {
                var id = _availableId.Pop();
                _overallBlocks[id] = customer;
                return id;
            }
            _overallBlocks[_lastId++] = customer;
            return _lastId - 1;
        }

        public void DeleteCustomer(int id)
        {
            if (!_overallBlocks.ContainsKey(id))
                return;
            
            _availableId.Push(id);
            _overallBlocks.Remove(id);
        }

        public bool TryUpdateCustomer(int id, OverallBlock newCustomer)
        {
            if (!_overallBlocks.ContainsKey(id))
                return false;
            _overallBlocks[id] = newCustomer;
            return true;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            _overallBlocks.Clear();
        }
    }
}