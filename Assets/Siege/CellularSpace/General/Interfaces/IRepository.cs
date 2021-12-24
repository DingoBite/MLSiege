using System.Collections.Generic;

namespace Assets.Siege.CellularSpace.General.Interfaces
{
    public interface IRepository<T>
    {
        public int PeekId { get; }
        public bool TryGetCustomer(int id, out T customer);
        public int InsertCustomer(T customer);
        public void DeleteCustomer(int id);
        public bool TryUpdateCustomer(int id, T newCustomer);
        public void Save();
        public void Clear();
        public IEnumerable<T> GetCustomers();
    }

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