﻿using System.Collections.Generic;

namespace Assets.Siege.Model.BlockSpace.General.Interfaces
{
    public interface IRepository<T>
    {
        public int PeekId { get; }
        public IEnumerable<T> GetCustomers();
        public bool TryGetCustomerById(int id, out T customer);
        public int InsertCustomer(T customer);
        public void DeleteCustomer(int id);
        public bool TryUpdateCustomer(int id, T newCustomer);
        public void Save();
        public void Clear();
    }
}