using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Interfaces
{
    public interface ISearch
    {
        Task <List<Customer>>ConvertQuery(string query);
        Task<List<Customer>> SearchCustomerName(string name);
        Task<List<Customer>> SearchCustomerPhone(string num);
        Task<List<Customer>> SearchCustomerEmail(string email);

    }
}
