using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Member_Portal.Models
{
    public class SubscriptionDetails
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime SubscriptionDate { get; set; }
        [Required]
        public string RefillOccurrence { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public string MemberLocation { get; set; }
        [Required]
        public int PrescriptionId { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
