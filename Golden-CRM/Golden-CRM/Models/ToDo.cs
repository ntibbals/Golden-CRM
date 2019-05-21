using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models
{
    public class ToDo
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string UserID { get; set; }
        public string AssignedOwner { get; set; }
        public ContactTypes ContactType { get; set; }
        [Column(TypeName = "varchar(max)")]
        [MaxLength]
        public string Comment { get; set; }
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public bool Complete { get; set; }
        public DateTime CompletedDate { get; set; }

    }

}
