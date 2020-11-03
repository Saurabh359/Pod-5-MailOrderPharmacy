using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Member_Portal.Models
{
    public class DrugDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public DateTime ManufacturedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int Quantity { get; set; }
    }
}
