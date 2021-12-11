using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Blocks.Repositories
{
    public class FrameBlockRepository: IRepository<FrameBlock>
    {
        private readonly Dictionary<int, FrameBlock> _frameBlocks;
        private int _lastId;
        private readonly Stack<int> _availableId;

        public FrameBlockRepository()
        {
            _frameBlocks = new Dictionary<int, FrameBlock>();
            _lastId = 0;
            _availableId = new Stack<int>();
        }

        public int PeekId => _availableId.Count >= 1 ? _availableId.Peek() : _lastId;

        public IEnumerable<FrameBlock> GetCustomers() => _frameBlocks.Values;

        public bool TryGetCustomerById(int id, out FrameBlock customer) => _frameBlocks.TryGetValue(id, out customer);

        public int InsertCustomer(FrameBlock customer)
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

        public bool TryUpdateCustomer(int id, FrameBlock newCustomer)
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