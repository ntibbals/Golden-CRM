using Golden_CRM.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Components
{
    public class ToDos : ViewComponent
    {
        private readonly GoldenDbContext _context;
        public ToDos(GoldenDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var toDo = _context.ToDos.Where(t => t.CustomerID == id).ToList();

            if(toDo != null)
            {
                return View(toDo);
            }

            return View(null);
        }
    }
}
