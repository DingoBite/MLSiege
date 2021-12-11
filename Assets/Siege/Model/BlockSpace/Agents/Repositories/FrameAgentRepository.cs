using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Agents.Repositories
{
    public class FrameAgentRepository : IRepository<FrameAgent>
    {
        private readonly Dictionary<int, FrameAgent> _frameBlocks;
        private int _lastId;
        private readonly Stack<int> _availableId;

        public FrameAgentRepository()
        {
            _frameBlocks = new Dictionary<int, FrameAgent>();
            _lastId = 0;
            _availableId = new Stack<int>();
        }

        public int PeekId => _availableId.Count >= 1 ? _availableId.Peek() : _lastId;

        public IEnumerable<FrameAgent> GetCustomers() => _frameBlocks.Values;

        public bool TryGetCustomerById(int id, out FrameAgent customer) => _frameBlocks.TryGetValue(id, out customer);

        public int InsertCustomer(FrameAgent customer)
        {
            if (_availableId.Count >= 1)
            {
                var id = _availableId.Pop();
                _frameBlocks[id] = customer;
                return id;
            }
            _frameBlocks[_lastId++] = customer;
            return _lastId - 1;
        }

        public void DeleteCustomer(int id)
        {
            if (!_frameBlocks.ContainsKey(id))
                return;

            _availableId.Push(id);
            _frameBlocks[id].Dispose();
            _frameBlocks.Remove(id);
        }

        public bool TryUpdateCustomer(int id, FrameAgent newCustomer)
        {
            if (!_frameBlocks.ContainsKey(id))
                return false;
            _frameBlocks[id] = newCustomer;
            return true;
        }

        public void Save() => Debug.Log(_frameBlocks);

        public void Clear()
        {
            foreach (var overallBlock in _frameBlocks)
            {
                overallBlock.Value.Dispose();
            }

            _lastId = 0;
            _availableId.Clear();
            _frameBlocks.Clear();
        }
    }
}