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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Member_Portal.Controllers
{
    public class SubscriptionController : Controller
    {
        static List<SubscriptionDetails> details = new List<SubscriptionDetails>()
        {
            new SubscriptionDetails{Id=1, MemberId=1, MemberLocation="Haldwani", PrescriptionId=2, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
            new SubscriptionDetails{Id=2, MemberId=2, MemberLocation="Haldwani", PrescriptionId=3, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
            new SubscriptionDetails{Id=3, MemberId=1, MemberLocation="Haldwani", PrescriptionId=2, RefillOccurrence="Weekly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
            new SubscriptionDetails{Id=4, MemberId=3, MemberLocation="Haldwani", PrescriptionId=3, RefillOccurrence="Monthly", SubscriptionDate=Convert.ToDateTime("2020-11-24 12:12:00 PM"), Status=true },
        };

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(SubscriptionController));

        private IConfiguration configuration;

        public SubscriptionController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Warn("Anonymous user");
                return RedirectToAction("Index", "Home");
            }

            _log4net.Info("Display Member Subscriptions");

            // List of all subscriptions
            int MemberId = (int)HttpContext.Session.GetInt32("MemberId");
            var subs = details.FindAll(x => x.MemberId.Equals(MemberId));
            return View(subs);
        }

        public IActionResult Subscribe()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Warn("Anonymous user trying to Subscribe");
                return RedirectToAction("Index", "Home");
            }

            _log4net.Debug("Subscribe Page");
            return View();
        }

        [HttpPost]
        public IActionResult Subscribe([Bind("InsurancePolicyNumber,InsuranceProvider,PrescriptionDate,DrugName,DoctorName,RefillOccurrence")]PrescriptionDetails prescription)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
               _log4net.Warn("Anonymous user trying to Subscribe");
                return RedirectToAction("Index", "Home");
            }
            
            // Get MemberId from Session

            SubscriptionDetails result = new SubscriptionDetails();
            int id = (int)HttpContext.Session.GetInt32("MemberId");
            string policy = "sad";

            // Call Subscription Service -- Subscribe method with prescription details and policy details and Member Id

                
            
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(prescription), Encoding.UTF8, "application/json");

                string url = "" + configuration["ServiceUrls:Subscription"] + "PostSubscribe";

                var request = new HttpRequestMessage(HttpMethod.Post, url+"/" + policy + "/" + id)
                {
                    Content = content
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        _log4net.Error("False Response");
                        string message = "Something Went Wrong "+ response.StatusCode;
                        return RedirectToAction("ResponseDisplay","Subscription", new { message });
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<SubscriptionDetails>(data);

                    if(!result.Status)
                    {
                        string message = "Subscription failed due to InAvailability of Drugs ";
                        return RedirectToAction("ResponseDisplay", "Subscription",new { message } );
                    }

                    details.Add(result);
                    return RedirectToAction("Index");
                }
            }

        }

        public IActionResult UnSubscribe(int id)
        {
            
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("Token")))
            {
                _log4net.Warn("Anonymous access to Unsubscribe");
                return RedirectToAction("Index", "Home");
            }
            
            SubscriptionDetails result = new SubscriptionDetails();
            int MemberId = (int)HttpContext.Session.GetInt32("MemberId");

              // call subscription microservice

            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject("hello"), Encoding.UTF8, "application/json");

                string url = "" + configuration["ServiceUrls:Subscription"] + "PostUnSubscribe";

                var request = new HttpRequestMessage(HttpMethod.Post, url+"/" + MemberId + "/" + id)
                {
                    Content = content
                };

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));

                using (var response = httpClient.SendAsync(request).Result)
                {

                    if (!response.IsSuccessStatusCode)
                    {
                        string message = "Something Went Wrong "+ response.StatusCode;
                        return RedirectToAction("ResponseDisplay","Subscription", new { message });
                    }

                    var data = response.Content.ReadAsStringAsync().Result;

                    result = JsonConvert.DeserializeObject<SubscriptionDetails>(data);

                    if (result.Status)
                    {
                        string message = "Subscription failed due to pending refill dues ";
                        return RedirectToAction("ResponseDisplay", "Subscription", new { message });
                    }

                    var ob = details.Find(x=>x.Id==id);
                    details.Remove(ob);
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
