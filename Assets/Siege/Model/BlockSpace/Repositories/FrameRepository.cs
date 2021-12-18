using System;
using System.Collections.Generic;
using Assets.Siege.Model.BlockSpace.General.Interfaces;
using UnityEngine;

namespace Assets.Siege.Model.BlockSpace.Repositories
{
    public class FrameRepository<TFrame> : IRepository<TFrame> where TFrame : IDisposable
    {
        private readonly Dictionary<int, TFrame> _frameBlocks;
        private int _lastId;
        private readonly Stack<int> _availableId;

        public FrameRepository()
        {
            _frameBlocks = new Dictionary<int, TFrame>();
            _lastId = 0;
            _availableId = new Stack<int>();
        }

        public int PeekId => _availableId.Count >= 1 ? _availableId.Peek() : _lastId;

        public IEnumerable<TFrame> GetCustomers() => _frameBlocks.Values;

        public bool TryGetCustomer(int id, out TFrame customer) => _frameBlocks.TryGetValue(id, out customer);

        public int InsertCustomer(TFrame customer)
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

        public bool TryUpdateCustomer(int id, TFrame newCustomer)
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