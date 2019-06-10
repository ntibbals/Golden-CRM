using Golden_CRM.Models.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models
{
    public class Customer
    {

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public string AssignedOwner { get; set; }
        public DateTime LastVisited { get; set; }
        public string LastVisitedBy { get; set; }
        public Priorities Priority { get; set; }

        //Navigation Properties
        public ICollection<Note> Notes { get; set; }
    
    }
    public enum Priorities
    {
        High = 1,
        Medium = 2,
        Low = 3
    }
}
