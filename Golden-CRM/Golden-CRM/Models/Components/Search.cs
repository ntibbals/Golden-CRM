using Golden_CRM.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Components
{
    public class Search :ViewComponent
    {
        private readonly GoldenDbContext _context;

        public Search(GoldenDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

                return View();

        }
    }
}
