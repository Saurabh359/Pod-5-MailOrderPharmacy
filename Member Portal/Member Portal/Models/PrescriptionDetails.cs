﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Member_Portal.Models
{
    public class PrescriptionDetails
    {
        public int Id { get; set; }
        [Required]
        public int InsurancePolicyNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string InsuranceProvider { get; set; }
        [Required]
        public DateTime PrescriptionDate { get; set; }
        [Required]
        [StringLength(255)]
        public string DrugName { get; set; }
        [Required]
        [StringLength(255)]
        public string DoctorName { get; set; }
        [Required]
        public string RefillOccurrence { get; set; }
    }
}
