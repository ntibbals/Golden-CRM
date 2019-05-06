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
            var note = _context.Notes.Where(n => n.CustomerID == id).ToList();

            if (note != null)
            {
                return View(note);
            }
            return View(null);
        }

    }
}
