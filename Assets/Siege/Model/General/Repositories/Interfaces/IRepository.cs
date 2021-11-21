using System.Collections.Generic;

namespace Assets.Siege.Model.General.Repositories.Interfaces
{
    public interface IRepository<T> 
    {
        public IEnumerable<T> GetCustomers();
        public bool TryGetCustomerById(int id, out T customer);
        public int InsertCustomer(T customer);
        public void DeleteCustomer(int id);
        public bool TryUpdateCustomer(int id, T newCustomer);
        public void Save();
    }
}