using System.Collections.Generic;

namespace Assets.Siege.Model.BlockSpace.General.Interfaces
{
    public interface IRepository<in TKey, TValue>
    {
        public TValue this[TKey id] { get; set; }
        public bool ContainsKey(TKey key);
        public bool TryGetCustomer(TKey key, out TValue customer);
        public void Save();
        public void Remove(TKey key);
        public void Clear();
        public IEnumerable<TValue> GetCustomers();
    }
}