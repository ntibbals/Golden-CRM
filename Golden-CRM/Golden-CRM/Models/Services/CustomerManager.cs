using Golden_CRM.Data;
using Golden_CRM.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace Golden_CRM.Models.Services
{
    public class CustomerManager : ICustomer
    {
        private readonly GoldenDbContext _context;
        private readonly UserDbContext _userManager;

        public CustomerManager(GoldenDbContext context, UserDbContext userManager)
        {
            _context = context;
            _userManager = userManager;
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

        /// <summary>
        /// Find customers based on id
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>Customer</returns>
        public async Task<Customer> FindCustomer(int id)
        {
            Customer customer = await _context.Customers.FirstOrDefaultAsync(c => c.ID == id);

            return customer;
        }

        /// <summary>
        /// Get all customers based on user ID
        /// </summary>
        /// <param name="userID">user ID</param>
        /// <returns>list of all customers</returns>
        public async Task<List<Customer>> GetCustomers(string userID)
        {
            return await _context.Customers.Where(c => c.AssignedOwner == userID).ToListAsync();
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        /// <summary>
        /// Get a list of 10 most recent customers by user id
        /// </summary>
        /// <param name="userID">user ID</param>
        /// <returns>list of last 10 customers</returns>
        public async Task<List<Customer>> RecentCustomers(string userID)
        {
            var myCustomers = _context.Customers.Where(c => c.AssignedOwner == userID);
            var recent = myCustomers.OrderByDescending(o => o.LastVisited).Take(10).ToList();
            return recent;
        }

        /// <summary>
        /// Save customer
        /// </summary>
        /// <param name="customer">customer</param>
        /// <returns></returns>
        public async Task SaveAsync(Customer customer)
        {
            Customer cust = await _context.Customers.FirstOrDefaultAsync(c => customer.ID == customer.ID);

            if(cust == null)
            {
                _context.Customers.Add(customer);
            }

            else
            {
                cust = customer;
                _context.Customers.Update(cust);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get list of users
        /// </summary>
        /// <returns></returns>
        public async Task<List<ApplicationUser>> GetUsers()
        {

            //var users = _userManager.Users.Where(x => x.Roles.Select(y => y.Id).Contains(ddlRole)).ToList();
            //var role = _userManager.Roles.SingleOrDefault(m => m.Name == "role");
            //var usersInRole = _userManager.Users.Where(m => m.Roles.Any(r => r.RoleId == role.Id));
            var allUsers = _userManager.Users.ToList();


            return allUsers;
        }

        /// <summary>
        /// Upload a list of new customers
        /// </summary>
        /// <param name="csv">csv file</param>
        /// <returns></returns>
        public async Task Upload(string csv)
        {

            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, csv); /// combines root paths to read file in
            string[] parsedResults = File.ReadAllLines(fullPath); /// reads file into an array

            foreach (string item in parsedResults.Skip(1)) /// runs through for each, skipping initial title line in document
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] column = item.Split(","); ///splits array into comma delimited array
                    await SaveAsync(new Customer
                    {
                        FirstName = column[0], 
                        LastName = column[1],
                        Email = column[2],
                        PhoneNumber = FormatPhoneNumber(column[3]),
                    });
                }
            }
        }

        private string FormatPhoneNumber(string phoneNum)
        {
            string phoneFormat = "(###) ###-####";

            Regex regexObj = new Regex(@"[^\d]");
            phoneNum = regexObj.Replace(phoneNum, "");
            if (phoneNum.Length > 0)
            {
                phoneNum = Convert.ToInt64(phoneNum).ToString(phoneFormat);
            }
            return phoneNum;
        }
    }
}
