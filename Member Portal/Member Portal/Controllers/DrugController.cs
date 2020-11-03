using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Member_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Member_Portal.Controllers
{
    public class DrugController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Drug(string name)
        {
            DrugDetails drug;

            //send Drug Name
            // access drug details from Drug MicroService --  getDrugByName Method

            drug = new DrugDetails
            {
                Id = 9,
                Name = name,
                Quantity = 210,
                ExpiryDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),
                Manufacturer = "asddads",
                ManufacturedDate = Convert.ToDateTime("2020-11-24 12:12:00 PM")
            };

            return View(drug);
        }
    }
}
