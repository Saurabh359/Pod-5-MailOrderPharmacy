using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Member_Portal.Models;
using Microsoft.AspNetCore.Mvc;

namespace Member_Portal.Controllers
{
    public class SubscriptionController : Controller
    {
        private List<SubscriptionDetails> details = new List<SubscriptionDetails>()
        {
            new SubscriptionDetails{Id=1, MemberId=1, MemberLocation="Haldwani", PrescriptionId=2, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
            new SubscriptionDetails{Id=2, MemberId=1, MemberLocation="Haldwani", PrescriptionId=3, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
        };
        public IActionResult Index()
        {
            // List of all subscriptions
            return View(details);
        }

        public IActionResult Subscribe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Subscribe([Bind(include: "InsurancePolicyNumber,InsuranceProvider,PrescriptionDate,DrugName,DoctorName,RefillOccurrence")]PrescriptionDetails prescription)
        {
            bool success = true;

            // Get MemberId from Session
            // Call Subscription Service -- Subscribe method with prescription details and Member Id

            if(success)
            {
                return RedirectToAction("Index");
            }


            string message = "Subscription not possible";

            return RedirectToAction("ResponseDisplay",message);
        }

        public IActionResult UnSubscribe(int id)
        {
            bool success = false;

            // Call Subscription Service -- UnSubscribe method with Subscription Id 

            if(success)
                return RedirectToAction("Index");

            string message = "Unsubscribe Not Possible due to pending Refill Dues";

            return RedirectToAction("ResponseDisplay",message); 
        }

        public IActionResult ResponseDisplay(string message)
        {
            ViewBag.Response = message;

            return View();
        }
    }
}
