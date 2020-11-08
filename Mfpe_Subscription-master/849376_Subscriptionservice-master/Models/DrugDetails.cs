using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionService.Models
{
    public class DrugDetails
    {
        public int Id { get; set; }

        [Required]

        public string Name { get; set; }
        [Required]
        //  [Display(Name = "ManufactureDate")]

        public DateTime ManufacturedDate { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string Manufacturer { get; set; }

        public int Quantity { get; set; }

    }
}
