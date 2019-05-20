using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Golden_CRM.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Golden_CRM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomer _context;

            public HomeController(ICustomer context)
        {
            _context = context;
        }

        [Authorize(Policy = "MemberOnly")]
        public async Task<IActionResult> Index()
        {
            var customers = await _context.GetCustomers();
            return View(customers);
        }
    }
}