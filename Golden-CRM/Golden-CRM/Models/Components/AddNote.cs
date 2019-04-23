using Golden_CRM.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Components
{
    public class AddNote : ViewComponent
    {

        private readonly GoldenDbContext _context;

        public AddNote(GoldenDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            Note note = new Note();
            note.CustomerID = id;

            if (note != null)
            {
                return View(note);
            }
            return View();
        }
    }
}
