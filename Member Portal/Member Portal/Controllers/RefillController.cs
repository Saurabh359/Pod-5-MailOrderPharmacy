using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Member_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Member_Portal.Controllers
{
    public class RefillController : Controller
    {
        public IActionResult RefillStatus(int id)
        {
            RefillOrderDetails refillOrder;

            //send Subscription Id 

            //Call Refill Microservice -- LatestRefill Method
            //Return RefillOrderDetails 

            refillOrder = new RefillOrderDetails { Id = 3,
                                                   DrugQuantity = 7,
                                                   RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),
                                                   RefillDelivered = true,
                                                   Payment = true
                                                 };

            return View(refillOrder);
        }

        public IActionResult RefillDues(int id)
        {
            int due;

            //send Subscription Id

            //Call Refill Microservice -- DueRefills Method
            //Return Refill Counts

            due = 4;
            ViewBag.DueCount = due;

            return View();
        }

        public IActionResult AdhocRefill(int id)
        {
            bool success = true;
            RefillOrderDetails refillOrder;

            //get Member Id From Session

            //send Policy Id
            //send Member Id
            //send Subscription Id

            //Call Refill Microservice -- Adhoc Method
            //Return RefillOrderDetails

            if (success)
            {
                refillOrder = new RefillOrderDetails
                {
                    Id = 5,
                    DrugQuantity = 7,
                    RefillDate = Convert.ToDateTime("2020-11-24 12:12:00 PM"),
                    RefillDelivered = false,
                    Payment = false
                };

                return View(refillOrder);
            }

            string message = "Drug Not Available";

            return RedirectToAction("ResponseDisplay","Subscription",message);

        }
    }
}
