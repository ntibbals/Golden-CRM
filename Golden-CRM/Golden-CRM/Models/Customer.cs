using Golden_CRM.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models
{
    public class Customer
    {

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AssignedOwner { get; set; }

        //Navigation Properties
        public ICollection<Note> Notes { get; set; }
    
    }
}
