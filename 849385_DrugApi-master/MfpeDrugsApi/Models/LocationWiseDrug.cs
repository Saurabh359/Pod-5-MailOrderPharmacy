using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MfpeDrugsApi.Models
{
    public class LocationWiseDrug
    {
        public int Id { get; set; }


        public string Name { get; set; }
        

        public DateTime ManufacturedDate { get; set; }
        
        public DateTime ExpiryDate { get; set; }
       
        public string Manufacturer { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }
    }
}
