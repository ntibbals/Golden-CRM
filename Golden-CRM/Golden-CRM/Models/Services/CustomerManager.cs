using Golden_CRM.Data;
using Golden_CRM.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Services
{
    public class CustomerManager : ICustomer
    {
        private readonly GoldenDbContext _context;

        public CustomerManager(GoldenDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="id">customer ID</param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if(customer != null)
            {
                _context.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Customer> FindCustomer(int id)
        {
            Customer customer = await _context.Customers.FirstOrDefaultAsync(c => c.ID == id);

            return customer;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task SaveAsync(Customer customer)
        {
            Customer cust = await _context.Customers.FirstOrDefaultAsync(c = customer.ID == customer.ID);

            if(cust == null)
            {
                _context.Customers.Add(cust);
            }

            else
            {
                cust = customer;
                _context.Customers.Update(cust);
            }

            await _context.SaveChangesAsync();
        }
    }
}
