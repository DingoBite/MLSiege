using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Scripts.General.Repos
{
    public class IdRepository<TValue> : IIdRepository<TValue>
    {
        private int _endId;
        private readonly Stack<int> _availableIds = new Stack<int>();
        private readonly Dictionary<int, TValue> _values = new Dictionary<int, TValue>();

        public int PeekId() => _availableIds.Count > 0 ? _availableIds.Peek() : _endId;
        
        private int PopId() => _availableIds.Count > 0 ? _availableIds.Pop() : _endId++;
        
        public bool Contains(int id) => _values.ContainsKey(id);

        public bool Contains(TValue updatable) => _values.ContainsValue(updatable);

        public TValue Get(int id) => _values[id];

        public int Add(TValue value)
        {
            lock (_values)
            {
                var id = PopId();
                _values.Add(id, value);
                return id;
            }
        }

        public void Remove(int id)
        {
            lock (_values)
            {
                if (!_values.ContainsKey(id))
                    return;
                _values.Remove(id);
                _availableIds.Push(id);
            }
        }
        
        public void Remove(TValue value)
        {
            lock (_values)
            {
                foreach (var valuePair in _values.Where(valuePair => valuePair.Value.Equals(value)))
                {
                    _values.Remove(valuePair.Key);
                    _availableIds.Push(valuePair.Key);
                    return;
                }
            }
        }

        public IEnumerable<TValue> GetValues() => _values.Values;
    }
}