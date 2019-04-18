using Golden_CRM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Components
{
    public class Notes : ViewComponent
    {
        private readonly GoldenDbContext _context;

        public Notes(GoldenDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var customer = _context.Notes.Where(n => n.ID == id).ToList();

            if (customer != null)
            {
                return View(customer);
            }
            return View(null);
        }

    }
}
