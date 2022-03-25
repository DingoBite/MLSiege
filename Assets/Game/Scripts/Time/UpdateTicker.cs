using System.Collections.Generic;
using Game.Scripts.General.Repos;
using Game.Scripts.Time.Interfaces;
using UnityEngine;

namespace Game.Scripts.Time
{
    public class UpdateTicker : MonoBehaviour, IUpdateTicker
    {
        private readonly IdRepository<IUpdatable> _updatableRepository = new IdRepository<IUpdatable>();
        
        private void Update()
        {
            foreach (var updatable in _updatableRepository.GetValues())
            {
                updatable?.OnUpdate();
            }
        }

        public bool Contains(int id) => _updatableRepository.Contains(id);

        public bool Contains(IUpdatable updatable) => _updatableRepository.Contains(updatable);

        public int AddUpdatable(IUpdatable updatable) => _updatableRepository.Add(updatable);

        public void RemoveUpdatable(int id) => _updatableRepository.Remove(id);

        public void RemoveUpdatable(IUpdatable updatable) => _updatableRepository.Remove(updatable);
    }
}