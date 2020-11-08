using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SubscriptionService.Models
{
    public class PrescriptionDetails
    {
        public int Id { get; set; }
        public int InsurancePolicyNumber { get; set; }

        public string InsuranceProvider { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string DrugName { get; set; }
        public string DoctorName { get; set; }

        public string RefillOccurrence { get; set; }


       



    }
}
