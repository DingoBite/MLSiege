using Game.Scripts.General.Repos;
using Game.Scripts.Time.Interfaces;
using UnityEngine;

namespace Game.Scripts.Time
{
    public abstract class AbstractUpdateTicker : MonoBehaviour, IUpdateTicker
    {
        protected readonly IdRepository<IUpdatable> _updatableRepository = new IdRepository<IUpdatable>();
        public bool Contains(int id) => _updatableRepository.Contains(id);
        public bool Contains(IUpdatable updatable) => _updatableRepository.Contains(updatable);
        public int AddUpdatable(IUpdatable updatable) => _updatableRepository.Add(updatable);
        public void RemoveUpdatable(int id) => _updatableRepository.Remove(id);
        public void RemoveUpdatable(IUpdatable updatable) => _updatableRepository.Remove(updatable);

        protected abstract void OnUpdate();
        
        private void Update()
        {
            OnUpdate();
        }
    }
}