using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models
{
    public class Note
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string UserID { get; set; }
        [Column(TypeName = "varchar(max)")]
        [MaxLength]
        public string Comment { get; set; }
        public DateTime Date { get; set; }

    }


}
