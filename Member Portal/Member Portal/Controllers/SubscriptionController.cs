using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Member_Portal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        public IActionResult Subscribe([Bind("InsurancePolicyNumber,InsuranceProvider,PrescriptionDate,DrugName,DoctorName,RefillOccurrence")]PrescriptionDetails prescription)
        {
           
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
               // _log4net.Info("Anonymous Log in try to add vehicle but redirected to login page");
                return RedirectToAction("Login", "User");
            }
       
            // Get MemberId from Session

            SubscriptionDetails result = new SubscriptionDetails();
            int id = (int)HttpContext.Session.GetInt32("MemberId");
            string policy = "sad";

            // Call Subscription Service -- Subscribe method with prescription details and policy details and Member Id

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(prescription), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync("https://localhost:44329/api/Subscribe/PostSubscribe" + policy+"/"+ id, content).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong";
                        return RedirectToAction("ResponseDisplay", message);
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<SubscriptionDetails>(data);

                    if(result.Status)
                    {
                        string message = "Subscription failed due to InAvailability of Drugs ";
                        return RedirectToAction("ResponseDisplay", message);
                    }

                    return RedirectToAction("Index");
                }
            }

        }

        public IActionResult UnSubscribe(int id)
        {
            
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                // _log4net.Info("Anonymous Log in try to add vehicle but redirected to login page");
                return RedirectToAction("Login", "User");
            }
            
            SubscriptionDetails result = new SubscriptionDetails();
            int MemberId = (int)HttpContext.Session.GetInt32("MemberId");

              // call subscription microservice

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject("hello"), Encoding.UTF8, "application/json");

                using (var response = httpClient.PostAsync("https://localhost:44329/api/Subscribe/PostUnSubscribe/" + MemberId + "/" + id, content).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong";
                        return RedirectToAction("ResponseDisplay", message);
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<SubscriptionDetails>(data);

                    if (result.Status)
                    {
                        string message = "Subscription failed due to pending refill dues ";
                        return RedirectToAction("ResponseDisplay", message);
                    }

                    return RedirectToAction("Index");
                }
            }
        }

        public IActionResult ResponseDisplay(string message)
        {
            ViewBag.Response = message;

            return View();
        }
    }
}
