using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Interfaces
{
    interface ICustomer
    {
        Task<Customer> FindCustomer(int id);
        Task<List<Customer>> GetCustomers();
        Task SaveAsync(Customer customer);
        Task DeleteAsync(int id);
    }
}
