using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Member_Portal.Models
{
    public class UserDetails
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Not a Valid Email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [StringLength(255)]
        public string Location { get; set; }
    }
}
