using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RefillApi.Models
{
    public class RefillOrder
    {
        public int Id { get; set; }
        public DateTime RefillDate { get; set; }
        public int DrugQuantity { get; set; }
        public bool RefillDelivered { get; set; }
        public bool Payment { get; set; }
        public int SubscriptionId { get; set; }
    }
}
