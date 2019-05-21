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
            /*********************** PATTERN MATCHING - NOT YET SUPPORTED *********************************************/
            //switch (query)
            //{
            //    case var value when new Regex(@"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").IsMatch(value):
            //        return await SearchCustomerPhone(query);
            //    case query.Contains("@"):
            //        return await SearchCustomerEmail(query);
            //    default:
            //        return await SearchCustomerName(query);


            //}
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
            List<Customer> results = new List<Customer>();
            for (int i = 0; i < split.Length; i++)
            {
                var result = await _context.Customers.FirstOrDefaultAsync(c => c.FirstName == split[i] || c.LastName == split[i]);
                if(!results.Contains(result))
                results.Add(result);
            }
            return results;
        }

        public async Task<List<Customer>> SearchCustomerPhone(string phoneNum)
        {
            string phoneFormat = "(###) ###-####";

            Regex regexObj = new Regex(@"[^\d]");
            phoneNum = regexObj.Replace(phoneNum, "");
            if (phoneNum.Length > 0)
            {
                phoneNum = Convert.ToInt64(phoneNum).ToString(phoneFormat);
            }
            return await _context.Customers.Where(c => c.PhoneNumber == phoneNum).ToListAsync();

        }
    }
}
