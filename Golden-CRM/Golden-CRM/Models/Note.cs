using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models
{
    public class Note
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        [Column(TypeName = "varchar(max)")]
        [MaxLength]
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
