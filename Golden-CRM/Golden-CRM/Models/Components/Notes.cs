using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.Components
{
    public class Notes : ViewComponent
    {

        public async Task<IViewComponentResult> InokeAsync()
        {
            return View();
        }
    }
}
