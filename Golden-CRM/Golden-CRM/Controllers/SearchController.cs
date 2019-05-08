using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Golden_CRM.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Golden_CRM.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearch _context;

        public SearchController(ISearch context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchResults(string query)
        {
            if(query == null)
            {
                return NotFound();
            }
            var customer = await _context.ConvertQuery(query);

            if(customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }
    }
}