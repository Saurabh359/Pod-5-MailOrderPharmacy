using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionService.Models
{
    public class SubscriptionDetails
    {
        public int Id { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public string RefillOccurrence { get; set; }
        public int MemberId { get; set; }
        public string MemberLocation { get; set; }
        public int PrescriptionId { get; set; }
        public bool Status { get; set; }



    }
}
