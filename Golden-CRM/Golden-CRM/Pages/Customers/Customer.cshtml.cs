using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Golden_CRM.Models;
using Golden_CRM.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
            if(Customer.FirstName != null)
            {
                Customer.LastVisited = DateTime.Now;
                await _customer.SaveAsync(Customer);

            }
        }

        public async Task<IActionResult> OnPost()
        {
            var customer = await _customer.FindCustomer(ID.GetValueOrDefault()) ?? new Customer();
            customer.FirstName = Customer.FirstName;
            customer.LastName = Customer.LastName;
            customer.Email = Customer.Email;
            customer.PhoneNumber = FormatPhoneNumber(Customer.PhoneNumber);
            customer.LastVisited = DateTime.Now;
            if(customer.AssignedOwner != null || customer.AssignedOwner != "Not Assigned")
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
        private string FormatPhoneNumber(string phoneNum)
        {
            string phoneFormat = "(###) ###-####";

            Regex regexObj = new Regex(@"[^\d]");
            phoneNum = regexObj.Replace(phoneNum, "");
            if (phoneNum.Length > 0)
            {
                phoneNum = Convert.ToInt64(phoneNum).ToString(phoneFormat);
            }
            return phoneNum;
        }
    }
}