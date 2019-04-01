using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Golden_CRM.Models;
using Golden_CRM.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Golden_CRM.Pages.Leads
{
    public class CustomerModel : PageModel
    {
        private readonly ICuser _customer;

        [BindProperty]
        public Customer Customer { get; set; }
        [FromRoute]
        public int? ID { get; set; }

        
        public void OnGet()
        {
        }
    }
}