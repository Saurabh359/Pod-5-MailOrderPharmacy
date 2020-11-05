using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            new SubscriptionDetails{Id=2, MemberId=2, MemberLocation="Haldwani", PrescriptionId=3, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
            new SubscriptionDetails{Id=3, MemberId=1, MemberLocation="Haldwani", PrescriptionId=2, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
            new SubscriptionDetails{Id=4, MemberId=3, MemberLocation="Haldwani", PrescriptionId=3, RefillOccurrence="Monthly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
        };

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscriptionController));
        
        public IActionResult Index()
        {
            _log4net.Info("Display Member Subscriptions");

            // List of all subscriptions
            int MemberId = (int)HttpContext.Session.GetInt32("MemberId");
            var subs = details.FindAll(x => x.MemberId.Equals(MemberId));
            return View(subs);
        }

        public IActionResult Subscribe()
        {
            _log4net.Info("Subscribe Page");
            return View();
        }

        [HttpPost]
        public IActionResult Subscribe([Bind("InsurancePolicyNumber,InsuranceProvider,PrescriptionDate,DrugName,DoctorName,RefillOccurrence")]PrescriptionDetails prescription)
        {
           
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
               _log4net.Info("Anonymous user trying to Subscribe");
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
                
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44329/api/Subscribe/PostSubscribe/" + policy + "/" + id)
                {
                    Content = content
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
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

                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:44329/api/Subscribe/PostUnSubscribe/" + MemberId + "/" + id)
                {
                    Content = content
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
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
