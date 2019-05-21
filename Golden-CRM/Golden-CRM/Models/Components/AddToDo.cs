using Golden_CRM.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Components
{
    public class AddToDo : ViewComponent
    {

        private readonly GoldenDbContext _context;

        public AddToDo(GoldenDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ToDo toDo = new ToDo();
            toDo.CustomerID = id;
            if(toDo != null)
            {
                return View(toDo);
            }
            return View();
        }
    }
}
