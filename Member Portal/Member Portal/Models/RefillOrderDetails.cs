using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Member_Portal.Models
{
    public class RefillOrderDetails
    { 
        public int Id { get; set; }
        public DateTime RefillDate { get; set; }
        public int DrugQuantity { get; set; }
        public bool RefillDelivered { get; set; }
        public bool Payment { get; set; }
    }
}
