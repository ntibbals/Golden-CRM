using Golden_CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Interfaces
{
    public interface ICustomer
    {
        Task<Customer> FindCustomer(int id);
        Task<List<Customer>> GetCustomers(string userID);
        Task SaveAsync(Customer customer);
        Task DeleteAsync(int id);
        Task<List<Customer>> RecentCustomers(string userID);
        Task<List<ApplicationUser>> GetUsers();

    }
}
