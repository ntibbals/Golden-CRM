﻿using System;
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


        public async Task<IActionResult> Index(string userID)
        {
            var customers = await _context.GetCustomers(userID);
            if(customers == null)
            {
                return View("empty");
            }

            return View(customers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var customer = await _context.FindCustomer(id);

            if(customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult Create()
        {
            return View();
        }

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

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _context.FindCustomer(id);

            return View(customer);
        }

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

        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.FindCustomer(id);

            if(customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _context.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Upload(string csv)
        {
            await _context.Upload(csv);
            return RedirectToAction(nameof(Index));
        }
    }
}