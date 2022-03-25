using System.Collections.Generic;
using Game.Scripts.Time.Interfaces;
using UnityEngine;

namespace Game.Scripts.Time
{
    public class UpdateTicker : MonoBehaviour, IUpdateTicker
    {
        private int _endId;
        private readonly Stack<int> _availableIds = new Stack<int>();
        private readonly Dictionary<int, IUpdatable> _updatableList = new Dictionary<int, IUpdatable>();
        
        private void Update()
        {
            foreach (var updatable in _updatableList.Values)
            {
                updatable?.OnUpdate();
            }
        }

        public bool Contains(int id) => _updatableList.ContainsKey(id);

        public bool Contains(IUpdatable updatable) => _updatableList.ContainsValue(updatable);

        public int AddUpdatable(IUpdatable updatable)
        {
            if (updatable == null) return -1;
            lock (_updatableList)
            {
                int id;
                if (_availableIds.Count > 0)
                {
                    id = _availableIds.Pop();
                    _updatableList[id] = updatable;
                }
                else
                {
                    id = _endId++;
                    _updatableList.Add(id, updatable);
                }
                return id;
            }
        }

        public void RemoveUpdatable(int id)
        {
            lock (_updatableList)
            {
                if (!_updatableList.ContainsKey(id))
                    return;
                _updatableList[id] = null;
                _availableIds.Push(id);
            }
        }

        public void RemoveUpdatable(IUpdatable updatable)
        {
            lock (_updatableList)
            {
                foreach (var updatablePair in _updatableList)
                {
                    if (updatablePair.Value != updatable) continue;
                    
                    _updatableList[updatablePair.Key] = null;
                    _availableIds.Push(updatablePair.Key);
                    return;
                }
            }
        }
    }
}