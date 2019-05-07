using Golden_CRM.Data;
using Golden_CRM.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Services
{
    public class SearchManager : ISearch
    {

        private readonly GoldenDbContext _context;
        public SearchManager(GoldenDbContext context)
        {
            _context = context;
        }
        public async Task<List<Customer>> ConvertQuery(string query)
        {
            if (Regex.Match(query, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success)
            {
               return await SearchCustomerPhone(query);
            }
            else if(query.Contains("@"))
            {
                return await SearchCustomerEmail(query);
            }
            else
            {
                return await SearchCustomerName(query);
            }

        }

        public async Task<List<Customer>> SearchCustomerEmail(string email)
        {
            return await _context.Customers.Where(c => c.Email == email).ToListAsync();

        }

        public async Task<List<Customer>> SearchCustomerName(string name)
        {

            string[] split = name.Split(" ");
            var FName = await _context.Customers.Where(c => c.FirstName == name[0].ToString() || c.LastName == name[0].ToString() || c.LastName == name[1].ToString()).ToListAsync();
            return FName;
        }

        public async Task<List<Customer>> SearchCustomerPhone(string num)
        {
            return await _context.Customers.Where(c => c.PhoneNumber == num).ToListAsync();

        }
    }
}
