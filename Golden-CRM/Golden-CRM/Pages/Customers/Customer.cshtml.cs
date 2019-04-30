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
        private readonly ICustomer _customer;

        [BindProperty]
        public Customer Customer { get; set; }

        [FromRoute]
        public int? ID { get; set; }

        public CustomerModel(ICustomer customer)
        {
            _customer = customer;
        }
        public async Task OnGetAsync()
        {
            Customer = await _customer.FindCustomer(ID.GetValueOrDefault()) ?? new Customer();
        }

        public async Task<IActionResult> OnPost()
        {
            var customer = await _customer.FindCustomer(ID.GetValueOrDefault()) ?? new Customer();
            customer.FirstName = Customer.FirstName;
            customer.LastName = Customer.LastName;
            customer.Email = Customer.Email;
            customer.PhoneNumber = Customer.PhoneNumber;
            if(customer.AssignedOwner != null || customer.AssignedOwner == "Not Assigned")
            {
                customer.AssignedOwner = Customer.AssignedOwner;
            }
            else
            {
                customer.AssignedOwner = "Not Assigned";
            }

            await _customer.SaveAsync(customer);

            return RedirectToPage("/Customers/Customer", new { id = customer.ID });
        }

        public async Task<IActionResult> OnPostDelete()
        {
            await _customer.DeleteAsync(ID.Value);

            return RedirectToPage("/Index");
        }
    }
}