using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MfpeDrugsApi.Models
{
    public class DrugLocation
    {
      //  public int Id { get; set; }
        public int Id { get; set; }
        
        public string Location { get; set; }
        
        public int Quantity { get; set; }
        
        public string DrugName { get; set; }
    }
}
