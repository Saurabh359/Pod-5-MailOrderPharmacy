using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationMicroService.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string Token { get; set; }
    }
}
