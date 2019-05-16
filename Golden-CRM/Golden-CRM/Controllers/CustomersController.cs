using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Golden_CRM.Models;
using Golden_CRM.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Golden_CRM.Controllers
{
    public class CustomersController : Controller
    {

        private readonly ICustomer _context;

        public CustomersController(ICustomer context)
        {
            _context = context;
        }

        /// <summary>
        /// Customers Home page index view
        /// </summary>
        /// <param name="userID">user ID</param>
        /// <returns>User to Customer home page</returns>
        public async Task<IActionResult> Index(string userID)
        {
            var customers = await _context.GetCustomers(userID);
            if(customers == null)
            {
                return View("empty");
            }

            return View(customers);
        }

        /// <summary>
        /// Controlls customer details view - finds customer by id
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns>details view</returns>
        public async Task<IActionResult> Details(int id)
        {
            var customer = await _context.FindCustomer(id);

            if(customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        /// <summary>
        /// Method to create a view
        /// </summary>
        /// <returns>Create view</returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This method creates a customer in db
        /// </summary>
        /// <param name="customer">customer object</param>
        /// <returns>View of customer</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID, FirstName, LastName, Email, PhoneNumber")] Customer customer)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    await _context.SaveAsync(customer);
                    return RedirectToAction(nameof(Index));
                }
                return View(customer);
            }
            catch(Exception)
            {
                return Redirect("https://http.cat/500");
            }
        }

        /// <summary>
        /// This method contorls edit view
        /// </summary>
        /// <param name="id">customer id</param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.FindCustomer(id);

            return View(customer);
        }

        /// <summary>
        /// This method edits customer located in db
        /// </summary>
        /// <param name="id">id of existing customer</param>
        /// <param name="customer">new customer information</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID, FirstName, LastName, Email, PhoneNumber, LastVisited")]Customer customer)
        {
            if(id != customer.ID)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    customer.LastVisited= DateTime.Now;
                    await _context.SaveAsync(customer);
                }
                catch(DbUpdateConcurrencyException)
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        /// <summary>
        /// This method handles delete views
        /// </summary>
        /// <param name="id">customer id for delete</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.FindCustomer(id);

            if(customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        /// <summary>
        /// This method removes customer from db
        /// </summary>
        /// <param name="id">customer id to remove</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// This method uploads customers into db through csv file
        /// </summary>
        /// <param name="csv">csv file path</param>
        /// <returns></returns>
        public async Task<IActionResult> Upload(string csv)
        {
            await _context.Upload(csv);
            return RedirectToAction(nameof(Index));
        }
    }
}